using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XinYiThree.ViewModels
{
    public class UserAddViewModel
    {
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]
        public string  Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        [Required]

        public int SID { get; set; }

        [Required]

        public string IdCardNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
