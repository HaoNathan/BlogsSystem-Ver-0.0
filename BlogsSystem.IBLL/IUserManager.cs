using BlogsSystem.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace BlogsSystem.IBLL
{
    public interface IUserManage
    {
        Task Register(string email,string password);
        bool Login(string email, string password,out Guid userId);
        Task ChangeUserPassword(string email, string oldPwd, string newPwd);
        Task ChangeUserInformation(string email,string siteName,string imagePath);
        Task<Dto.UserInfromationDto> GetUserByEmail(string email);
    }
}
