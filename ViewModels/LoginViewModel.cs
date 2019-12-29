using System.ComponentModel.DataAnnotations;

namespace XinYiThree.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string  Username { get; set; }
        [Required]
        [Display(Name ="密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
