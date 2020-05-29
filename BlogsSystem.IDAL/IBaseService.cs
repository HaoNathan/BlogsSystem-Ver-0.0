using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogsSystem.MODEL;

namespace BlogsSystem.IDAL
{
    public interface  IBaseService<T>:IDisposable where T:BaseEntity
    {
        Task CreateAsync(T model,bool autoSave=true);
        Task EditAsync(T model, bool autoSave = true);
        Task RemoveAsync(Guid id, bool autoSave = true);
        Task RemoveAsync(T model, bool autoSave = true);
        Task Save();
        Task<T> GetOneByIdAsync(Guid id);
        IQueryable<T> GetAllAsync();
        IQueryable<T> GetAllByPageAsync(int pageSize=10,int pageIndex=0);
        IQueryable<T> GetOrderAsync(bool asc=true);
        IQueryable<T> GetAllByPageOrderAsync(int pageSize=10,int pageIndex=0,bool asc=true);

    }
}
