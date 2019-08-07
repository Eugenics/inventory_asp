using System;
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
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            var identityUsers = _userManager.Users.ToList();

            usersList = new List<InputModel>();

            foreach (var item in identityUsers)
            {
                usersList.Add(
                    new InputModel
                    {
                        UserId = item.Id,
                        UserName = item.UserName,
                        Role = Role,
                        Email = item.Email
                    });
            }
        }

        readonly List<InputModel> usersList;
        //public string Username { get; set; }
        public string Role { get; set; }
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

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //var product = await _context.Products.FindAsync(id);

            //if (product != null)
            //{
            //    _context.Products.Remove(product);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage();
        }
    }
}
