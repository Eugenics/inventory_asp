using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using inventory_dot_core.Classes;

namespace inventory_dot_core.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public List<ApplicationRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public EditModel(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            AllRoles = new List<ApplicationRole>();
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

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                //await _signInManager.RefreshSignInAsync(user);

                // update regions for current user
                userRoles = await _userManager.GetRolesAsync(user);
                List<string> _regions = new List<string>();
                
                if (userRoles != null)
                {
                    foreach (string role in userRoles)
                    {
                        if (role.Contains("users"))
                        {

                            if (role == "all_users")
                            {
                                _regions = new List<string>() { "22", "24", "38", "42", "54", "55", "70" };
                                break;
                            }

                            if (role == "54_users") _regions.Add("54");
                            else if (role == "22_users") _regions.Add("22");
                            else if (role == "24_users") _regions.Add("24");
                            else if (role == "38_users") _regions.Add("38");
                            else if (role == "42_users") _regions.Add("42");
                            else if (role == "54_users") _regions.Add("54");
                            else if (role == "55_users") _regions.Add("55");
                            else if (role == "70_users") _regions.Add("70");
                        }
                    }

                    if(_regions.Count > 0)
                    {
                        user.Regions = string.Join(',', _regions.ToArray());
                        await _userManager.UpdateAsync(user);
                    }
                    else
                    {
                        user.Regions = "";
                        await _userManager.UpdateAsync(user);
                    }
                }

                return RedirectToPage("/Index");
            }
            return NotFound();
        }
    }
}
