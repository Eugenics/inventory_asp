using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace inventory_dot_core.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public string Username { get; set; }

        public EditModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();

            AllRoles = _roleManager.Roles.ToList();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Индентификатор пользователя")]
            public string UserId { get; set; }

            [Required]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Электронный адрес")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Роль")]
            public string Role { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        public void OnGet(string userid)
        {
            var user = _userManager.Users
                .Where(x => x.Id == userid)
                .FirstOrDefault();
            // получем список ролей пользователя
            var userRole = _userManager.GetRolesAsync(user);
            UserRoles = userRole.Result;

            var userName = _userManager.GetUserNameAsync(user);
            var email = _userManager.GetEmailAsync(user);
            Username = userName.Result;

            Input = new InputModel
            {
                UserName = Username,
                Email = email.Result,
                //Role = role
            };
        }

        public void OnPost(string roleName)
        {
            //var addRoleResult = await _userManager.AddToRoleAsync(, roleName);
        }
    }
}
