using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using inventory_dot_core.Classes;

namespace inventory_dot_core.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                Response.Cookies.Delete(".AspNetCore.Identity.Application");
                return LocalRedirect(returnUrl);
            }
            else
            {

                //foreach (var cookie in Request.Cookies.Keys)
                //{
                //    Response.Cookies.Delete(".AspNetCore.Identity.Application");
                //}
                //Response.Cookies.Delete(".AspNetCore.Identity.Application");
                //return Page();
                return RedirectToPage();
            }
        }
    }
}