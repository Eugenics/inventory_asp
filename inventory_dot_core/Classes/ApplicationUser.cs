using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace inventory_dot_core.Classes
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// List of user allowed regions for using in dataset filters
        /// </summary>
        public List<string> UserRegions { get; set; }

        /// <summary>
        /// String of user allowed regions stored in database
        /// </summary>
        public string Regions { get; set; }        
    }
}
