using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_dot_core.Areas.Admin.Model
{
    public partial class User :IdentityUser
    {
        public string Role { get; set; }
    }
}
