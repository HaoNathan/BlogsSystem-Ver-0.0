using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogsSystem.MVCSite.Models.ArticleViewModels
{
    public class CreateArticleViewModels
    {
        [DisplayName("标题")]
        [Required]
        public string Title { get; set; }
        [DisplayName("主要内容")]
        [Required]
        public string Content { get; set; }
        [DisplayName("用户文章分类")]
        public Guid[] CategoryIds { get; set; }
    }
}