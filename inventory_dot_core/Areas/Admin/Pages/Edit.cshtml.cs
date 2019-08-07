using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory_dot_core.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EditModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet(string userid)
        {
            var user = _userManager.Users.Where(x => x.Id == userid).FirstOrDefault();

            var userName =  _userManager.GetUserNameAsync(user);
            //var email =  _userManager.GetEmailAsync(user);
            //var role = Role;

            //Username = userName;

            //Input = new InputModel
            //{
            //    UserName = userName,
            //    Email = email,
            //    Role = role
            //};
        }

        public void OnPost()
        {

        }
    }
}
