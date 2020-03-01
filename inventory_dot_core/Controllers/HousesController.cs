using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Authorization;
using SmartBreadcrumbs.Attributes;
using inventory_dot_core.Classes.Paging;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using System.Security.Claims;

// ReSharper disable All

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
    public class HousesController : Controller
    {
        private readonly InventoryContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private IList<string> userRoles;
        // Alowed regions for current user
        private List<string> regions;

        public HousesController(InventoryContext context,
                                UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            userRoles = new List<string>();
            regions = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "HousesId")
        {
            var housesesQueryable = _context.Houses.Include(h => h.HousesRegion).AsQueryable();
            int pageSize = 15;

            getRegions();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                housesesQueryable = housesesQueryable.Where(h => EF.Functions.Like(h.HousesName.ToUpper(), "%" + filter + "%")
                || EF.Functions.Like(h.HousesRegion.RegionName.ToUpper(), "%" + filter + "%"));
            }

            housesesQueryable = housesesQueryable.Where(h => regions.Contains(h.HousesRegion.ToString()));

            var model = await PagingList.CreateAsync(housesesQueryable, pageSize, page, sortExpression, "HousesId");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "sortExpression", sortExpression },
                { "page", page }
            };

            return View(model);
        }


        // GET: Houses/Details/5
        [Breadcrumb("Строения детали")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await _context.Houses
                .Include(h => h.HousesRegion)
                .FirstOrDefaultAsync(m => m.HousesId == id);
            if (houses == null)
            {
                return NotFound();
            }

            return View(houses);
        }

        // GET: Houses/Create
        [Breadcrumb("Строения создать")]
        public IActionResult Create()
        {
            ViewData["HousesRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View();
        }

        // POST: Houses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HousesId,HousesName,HousesRem,HousesRegionId")] Houses houses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(houses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HousesRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", houses.HousesRegionId);
            return View(houses);
        }

        // GET: Houses/Edit/5
        [Breadcrumb("Строения редактировать")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await _context.Houses.FindAsync(id);
            if (houses == null)
            {
                return NotFound();
            }
            ViewData["HousesRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", houses.HousesRegionId);
            return View(houses);
        }

        // POST: Houses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HousesId,HousesName,HousesRem,HousesRegionId")] Houses houses)
        {
            if (id != houses.HousesId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(houses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HousesExists(houses.HousesId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HousesRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", houses.HousesRegionId);
            return View(houses);
        }

        // GET: Houses/Delete/5
        [Breadcrumb("Строения удалить")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houses = await _context.Houses
                .Include(h => h.HousesRegion)
                .FirstOrDefaultAsync(m => m.HousesId == id);
            if (houses == null)
            {
                return NotFound();
            }

            return View(houses);
        }

        // POST: Houses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var houses = await _context.Houses.FindAsync(id);
            _context.Houses.Remove(houses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HousesExists(int id)
        {
            return _context.Houses.Any(e => e.HousesId == id);
        }

        private void getRegions()
        {
            var _userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var _user = _userManager.Users.Where(x => x.Id == _userId).FirstOrDefault();
            userRoles = _userManager.GetRolesAsync(_user).Result;

            foreach (string role in userRoles)
            {

                if (role == "all_users")
                {
                    regions = new List<string> { "22", "24", "38", "42", "54", "55", "70" };
                    break;
                }

                if (role == "54_users") regions.Add("54");
                else if (role == "22_users") regions.Add("22");
                else if (role == "24_users") regions.Add("24");
                else if (role == "38_users") regions.Add("38");
                else if (role == "42_users") regions.Add("42");
                else if (role == "54_users") regions.Add("54");
                else if (role == "55_users") regions.Add("55");
                else if (role == "70_users") regions.Add("70");
            }
        }
    }
}
