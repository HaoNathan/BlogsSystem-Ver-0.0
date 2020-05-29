using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BlogsSystem.IDAL;
using BlogsSystem.MODEL;

namespace BlogsSystem.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity,new ()
    {
        private readonly BlogContext _db;

        public BaseService(BlogContext db)
        {
            this._db = db;
        }

        public async Task CreateAsync(T model, bool autoSave = true)
        {
            _db.Set<T>().Add(model);
            if (autoSave) await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditAsync(T model, bool autoSave = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            _db.Entry(model).State = EntityState.Modified;
            if (autoSave)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public  IQueryable<T> GetAllAsync()
        {
            return _db.Set<T>().Where(m => !m.IsRemoved).AsNoTracking();
        }

        public  IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            return GetAllAsync().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public  IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return  GetOrderAsync(asc).Skip(pageSize*pageIndex).Take(pageSize);
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAllAsync().FirstAsync(predicate:m => m.Id == id);
        }

        public  IQueryable<T> GetOrderAsync(bool asc = true)
        {
            var list = GetAllAsync();
            list = asc ? list.OrderBy(m => m.CreatTime) : list.OrderByDescending(m => m.CreatTime);
            return list;
        }

        public async Task RemoveAsync(Guid id, bool autoSave = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T () { Id = id };
            _db.Entry(t).State = EntityState.Modified;
            t.IsRemoved = true;
            if (autoSave)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(T model, bool autoSave = true)
        {
            if (autoSave) await RemoveAsync(model.Id,autoSave);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}
