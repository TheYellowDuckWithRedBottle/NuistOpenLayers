using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XinYiThree.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            Users = new List<IdentityUser>();
        }
        public string UserId { get; set; }

        public string RoldId { get; set; }

        public List<IdentityUser> Users { get; set; }
    }
}
