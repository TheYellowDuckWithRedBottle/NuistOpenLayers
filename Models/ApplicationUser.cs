using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XinYiThree.Models
{
    public class ApplicationUser:IdentityUser
    {
       [Required]
        public int SID { get; set; }

        [Required]
        public string IdCardNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
