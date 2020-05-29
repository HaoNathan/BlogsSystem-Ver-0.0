using BlogsSystem.IDAL;
using BlogsSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.DAL
{
    public class ArticleToCategoryService : BaseService<MODEL.ArticleToCategory>, IArticleToCategory
    {
        public ArticleToCategoryService( ) : base(new BlogContext())
        {
        }
    }
}
