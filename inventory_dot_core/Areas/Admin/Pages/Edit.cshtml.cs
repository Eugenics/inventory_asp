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
        private readonly RoleManager<IdentityRole> _roleManager;
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
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

        /// <summary>
        /// 
        /// </summary>
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
            var userName = _userManager.GetUserNameAsync(user);
            var email = _userManager.GetEmailAsync(user);
            UserRoles = userRole.Result;
            Username = userName.Result;

            Input = new InputModel
            {
                UserId = user.Id,
                UserName = Username,
                Email = email.Result
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string userId, List<string> roles)
        {
            // получаем пользователя
            var user = await _userManager.FindByIdAsync(userId);
            //var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound($"Невозможно загрузить пользователя с идентификатором '{_userManager.GetUserId(User)}'.");

            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                var role = await _userManager.AddToRolesAsync(user, addedRoles);
                if (role.Succeeded) ;
                //await _userManager.remove(user, removedRoles);

                //await _signInManager.RefreshSignInAsync(user);

                return RedirectToPage("/Index");
            }
            return NotFound();
        }
    }
}
