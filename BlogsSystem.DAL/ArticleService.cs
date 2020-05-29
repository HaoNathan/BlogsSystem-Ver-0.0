using BlogsSystem.IDAL;
using BlogsSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.DAL
{
    public class ArticleService:BaseService<MODEL.Article>,IArticleService
    {
        public ArticleService() : base(new BlogContext())
        {

        }
       
        
    }
}
