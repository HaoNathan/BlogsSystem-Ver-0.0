using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 *类名称：CommentDto
 *类描述：
 *创建人：ASUS
 *创建时间：2020-05-21 10:32:05
 *修改人：ASUS
 *修改时间：2020-05-21 10:32:05
 */

namespace BlogsSystem.Dto
{
   public class CommentDto
    {
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid Id { get; set; }
        public string Comment { get; set; }
    }
}
