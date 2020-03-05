using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using inventory_dot_core.Classes;

namespace inventory_dot_core.Classes
{
    public class RegionFilter
    {
        public RegionFilter() { }

        public IQueryable<T> SetRegionFilter<T>(IQueryable<T> DataSet, UserManager<ApplicationUser> userManager, HttpContext httpContext, string regionStringName) where T : class
        {
            var _userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var _user = userManager.Users.Where(x => x.Id == _userId).FirstOrDefault();

            var _regions = _user.Regions.Split(',').ToList();
            if (_regions == null) _regions = new List<string>();            

            IQueryable<T> set = DataSet.Where(i => _regions.Contains(
                i.GetType().GetProperty(regionStringName).GetValue(i, null).ToString()
                ));

            return set;
        }
    } 
}
