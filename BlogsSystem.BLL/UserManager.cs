using BlogsSystem.DAL;
using BlogsSystem.Dto;
using BlogsSystem.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogsSystem.MODEL;
using System.Data.Entity;
using BlogsSystem.IDAL;
using System.Threading;

namespace BlogsSystem.BLL
{
    public class UserManager : IUserManage
    {
        
        public async Task ChangeUserInformation(string email, string siteName, string imagePath)
        {
            using (IUserService userSvc = new UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(m => m.Email == email ))
                {
                    var user = await userSvc.GetAllAsync().FirstAsync(m => m.Email == email);
                    user.SiteName = siteName;
                    user.Email = email;
                    user.ImagePath = imagePath;
                    await userSvc.EditAsync(user);
                }
            }
        }

        public async Task ChangeUserPassword(string email, string oldPwd, string newPwd)
        {
            using (IUserService userSvc=new UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(m=>m.Email==email&&m.Password==oldPwd))
                {
                    var user =await userSvc.GetAllAsync().FirstAsync(m=>m.Email==email);
                    user.Password = newPwd;
                    await userSvc.EditAsync(user);
                }
            }
        }

        public async Task<UserInfromationDto> GetUserByEmail(string email)
        {
            using (IUserService userSvc=new UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(m => m.Email == email))
                {
                    return await userSvc.GetAllAsync().Where(m => m.Email == email).Select(newUser =>
                    new UserInfromationDto
                    {
                        Id = newUser.Id,
                        Email = newUser.Email,
                        FansCount = newUser.FansCount,
                        FocusCount = newUser.FocusCount,
                        SiteName = newUser.SiteName,
                        ImagePath = newUser.ImagePath
                    }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException(message: "输入的邮箱不存在");
                }
            }
        }

        public  bool Login(string email, string password,out Guid userId)
        {
            using (IUserService userSvc = new UserService())
            {
                var  user=  userSvc.GetAllAsync().FirstOrDefaultAsync(m=>m.Email==email&&m.Password==password);
                user.Wait();
                var data = user.Result;
                if (data==null)
                {
                    userId = new Guid();
                    return false;
                }
                userId = data.Id;
                return true;
            }
        }

        public async Task Register(string email, string password)
        {
            using (IUserService userSvc = new UserService())
            {
                await userSvc.CreateAsync(new User()
                {
                    Email = email,
                    Password = password,
                    SiteName = "indexHome",
                    ImagePath = "~/images/index.png"
                });
            }
        }
    }
}
