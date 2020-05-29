using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.MODEL
{
    public class User:BaseEntity
    {
        [Required,StringLength(maximumLength:40),Column(TypeName ="varchar")]
        public string Email { get; set; }
        [Required,StringLength(maximumLength:20),Column(TypeName ="varchar")]
        public string Password { get; set; }
        [Required, StringLength(maximumLength: 100), Column(TypeName = "nvarchar")]
        public string ImagePath { get; set; }
        public int FansCount { get; set; }
        public int FocusCount { get; set; }
        public string SiteName { get; set; }
    }
}
