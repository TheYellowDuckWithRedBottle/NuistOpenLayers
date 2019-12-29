using System;
using System.ComponentModel.DataAnnotations;

namespace XinYiThree.ViewModels
{
    public class SignViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]
        public string Email { get; set; }

        [Required]
     
        public int SID { get; set; }

        [Required]
 
        public string IdCardNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
