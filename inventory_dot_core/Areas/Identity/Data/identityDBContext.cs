using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Classes;

namespace inventory_dot_core.Models
{
    public class IdentityDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ignoring UserRegions field for store in database
            builder.Entity<ApplicationUser>().Ignore(u => u.UserRegions);

            //builder.HasDefaultSchema("identity");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
