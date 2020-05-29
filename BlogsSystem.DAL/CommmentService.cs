using BlogsSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 *类名称：CommmentService
 *类描述：
 *创建人：ASUS
 *创建时间：2020-05-21 10:19:55
 *修改人：ASUS
 *修改时间：2020-05-21 10:19:55
 */

namespace BlogsSystem.DAL
{
    public class CommmentService : BaseService<Comment>, IDAL.ICommentService
    {
        public CommmentService() : base(new BlogContext())
        {
        }
    }
}
