using BlogsSystem.IDAL;
using BlogsSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.DAL
{
    public class UserService : BaseService<User>,IUserService
    {
        public UserService() : base(new BlogContext())
        {
        }
    }
}
