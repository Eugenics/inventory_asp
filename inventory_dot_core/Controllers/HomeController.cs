using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using inventory_dot_core.Models;
using SmartBreadcrumbs.Attributes;
using inventory_dot_core.Classes;
using Microsoft.AspNetCore.Authorization;

namespace inventory_dot_core.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IClock _clock;

        public HomeController(IClock clock)
        {
            _clock = clock;
        }

        [DefaultBreadcrumb("Домашняя")]
        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = $"It is {_clock.GetTime().ToString("T")}";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
