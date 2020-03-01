using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory_dot_core.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

            var identityUsers = _userManager.Users.ToList();
            usersList = new List<InputModel>();

            foreach (var item in identityUsers)
            {
                var user = _userManager.FindByIdAsync(item.Id);
                var userRole = _userManager.GetRolesAsync(item).Result;

                usersList.Add(
                    new InputModel
                    {
                        UserId = item.Id,
                        UserName = item.UserName,
                        Role = userRole,
                        Email = item.Email
                    });
            }
        }

        readonly List<InputModel> usersList;
        public List<InputModel> DisplayedUser { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "User Id")]
            public string UserId { get; set; }

            [Required]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Электронный адрес")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Роль")]
            public IList<string> Role { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            DisplayedUser = usersList;

            return Page();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IActionResult OnPostDelete(string userid)
        {
            var user = _userManager.FindByIdAsync(userid);
            _userManager.DeleteAsync(user.Result);

            return RedirectToPage();
        }
    }
}
