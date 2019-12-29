using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XinYiThree.Models;

namespace XinYiThree.Data
{
    public class XinyiDBContext: IdentityDbContext<ApplicationUser>
    {
        public XinyiDBContext(DbContextOptions<XinyiDBContext> options):base(options)
        {
            
        }
    }
}
