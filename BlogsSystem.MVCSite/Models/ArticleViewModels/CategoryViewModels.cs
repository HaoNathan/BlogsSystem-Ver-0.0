using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogsSystem.MVCSite.Models.ArticleViewModels
{
    public class CategoryViewModels
    {
        [Required]
        [StringLength(maximumLength:10,MinimumLength =2)]
        public string CategoryName { get; set; }
    }
}