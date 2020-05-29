using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BlogsSystem.MVCSite.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="邮箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name ="密码")]
        [Required]
        [DataType(dataType: DataType.Password)]
        [StringLength(maximumLength:20,MinimumLength =6)]
        public string Password { get; set; }
        [Display(Name ="记住密码")]
        public bool IsRemember { get; set; }
    }
}