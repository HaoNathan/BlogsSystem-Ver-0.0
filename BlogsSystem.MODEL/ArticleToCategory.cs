using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.MODEL
{
    public class ArticleToCategory:BaseEntity
    {
        [ForeignKey(nameof(BlogCategory))]
        public Guid ArticleCategoryId { get; set; } //全局唯一标识符,即不能重复.
        public BlogCategory BlogCategory { get; set; }
        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }
        public Article  Article { get; set; }
    }
}
