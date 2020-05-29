using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogsSystem.MVCSite.Models.UserViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("邮箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("密码")]
        [Required]
        [StringLength(maximumLength:30,MinimumLength =6)]
        public string Password { get; set; }
        [DisplayName("确认密码")]
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPwd { get; set; }
    }
}