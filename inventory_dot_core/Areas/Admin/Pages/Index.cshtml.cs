using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using inventory_dot_core.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory_dot_core.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public IndexModel(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
  
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
                        Role = userRole[0],
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
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Role")]
            public string Role { get; set; }
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

        public IActionResult OnPostDelete(int userid)
        {


            //var product = await _IdentityDBContext.

            //if (product != null)
            //{
            //    _context.Products.Remove(product);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage();
        }
    }
}
