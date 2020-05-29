using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogsSystem.MVCSite.Models.ArticleViewModels
{
    public class CreateCommentsViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Comment { get; set; }

    }
}