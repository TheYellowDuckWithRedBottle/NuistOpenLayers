﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XinYiThree.ViewModels
{
    public class RoleEditViewModel
    {
        [Required]
        [Display(Name = "角色ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

    }
}

