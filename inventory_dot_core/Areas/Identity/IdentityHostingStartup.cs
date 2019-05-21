﻿using System;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(inventory_dot_core.Areas.Identity.IdentityHostingStartup))]
namespace inventory_dot_core.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<identityDBContext>(options =>
                    options.UseNpgsql(context.Configuration.GetConnectionString("inventoryDataBase_test"))
                    );

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<identityDBContext>();
            });
        }
    }
}