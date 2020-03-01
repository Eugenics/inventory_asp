using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace inventory_dot_core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public LinkedList<string> UserRegions { get; set; }
    }
}
