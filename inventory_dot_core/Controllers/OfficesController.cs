using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using inventory_dot_core.Classes;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
    public class OfficesController : Controller
    {
        private readonly InventoryContext _context;
        private Classes.ControlesItems _ControleItems;
        private UserManager<ApplicationUser> _userManager;

        public OfficesController(InventoryContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _ControleItems = new Classes.ControlesItems(_context);
            _userManager = userManager;
        }

        // GET: Offices
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "OfficeId")
        {
            ViewBag.Filter = filter;
            ViewBag.SortExpression = sortExpression;

            var officesQueryable = _context.Offices
                .Include(o => o.OfficeHouses)
                .Include(r => r.OfficeHouses.HousesRegion)
                .AsQueryable();
            int pageSize = 15;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                officesQueryable = officesQueryable.Where(e => EF.Functions.Like(e.OfficeName .ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.OfficeHouses.HousesName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.OfficeHouses.HousesRegion.RegionName.ToUpper(), "%" + filter + "%")
                );
            }

            // Filter regions in dataset according user rights
            var _userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var _user = _userManager.Users.Where(x => x.Id == _userId).FirstOrDefault();

            var _regions = _user.Regions.Split(',').ToList();
            if (_regions == null) _regions = new List<string>();
            officesQueryable = officesQueryable.Where(o => _regions.Contains(o.OfficeHouses.HousesRegionId.ToString()));
            

            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync(officesQueryable, pageSize, page, sortExpression, "EmployeeId");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "sortExpression", sortExpression },
                { "page", page }
            };

            return View(model);
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices
                .Include(o => o.OfficeHouses)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (offices == null)
            {
                return NotFound();
            }

            return View(offices);
        }

        // GET: Offices/Create
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "EmployeeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            
            var _Region = _context.Region.FirstOrDefault();

            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            ViewData["OfficeHousesId"] = _ControleItems.GetHousesByRegion(_Region.RegionId);
            

            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfficeId,OfficeName,OfficeNotes,OfficeHousesId,OfficeIsStore")] Offices offices,
            bool OfficeIsStore,
            string filter = "", int page = 1, string sortExpression = "OfficeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            offices.OfficeIsStore = OfficeIsStore ? 1 : 0;

            ModelState.Clear();
            if (ModelState.IsValid)
            {
                _context.Add(offices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }

            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            ViewData["OfficeHousesId"] = new SelectList(_context.Houses,"HouseId","HouseName");

            return View(offices);
        }

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "OfficeId")
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var offices = await _context.Offices.FindAsync(id);

            if (offices == null)
            {
                return NotFound();
            }

            var _offices = _context.Offices.Include(h => h.OfficeHouses).AsNoTracking();

            _offices = _offices.Where(o => o.OfficeId == id);


            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            ViewData["OfficeHousesId"] = _ControleItems.GetHousesByRegion(_offices.First().OfficeHouses.HousesRegionId);

            return View(offices);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("OfficeId,OfficeName,OfficeNotes,OfficeHousesId,OfficeIsStore")] Offices offices,
            bool OfficeIsStore = false,
            string filter = "", int page = 1, string sortExpression = "OfficeId")
        {
            if (id != offices.OfficeId)
            {
                return NotFound();
            }

            offices.OfficeIsStore = OfficeIsStore ? 1 : 0;

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficesExists(offices.OfficeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }

            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            ViewData["OfficeHousesId"] = new SelectList(_context.Houses, "HouseId", "HouseName",offices.OfficeHousesId);
            return View(offices);
        }

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete(int? id,
            string filter = "", int page = 1, string sortExpression = "OfficeId")
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var offices = await _context.Offices
                .Include(o => o.OfficeHouses)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (offices == null)
            {
                return NotFound();
            }

            return View(offices);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,
            string filter = "", int page = 1, string sortExpression = "OfficeId")
        {
            var offices = await _context.Offices.FindAsync(id);
            _context.Offices.Remove(offices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),
                new
                {
                    filter = filter,
                    page = page,
                    sortExpression = sortExpression
                });
        }

        private bool OfficesExists(int id)
        {
            return _context.Offices.Any(e => e.OfficeId == id);
        }
    }
}
