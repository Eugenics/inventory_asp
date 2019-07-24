using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
    public class RelOfficeResponsEmployeesController : Controller
    {
        private readonly InventoryContext _context;
        private Classes.ControlesItems _ControleItems;

        public RelOfficeResponsEmployeesController(InventoryContext context)
        {
            _context = context;
            _ControleItems = new Classes.ControlesItems(_context);
        }

        // GET: RelOfficeResponsEmployees
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            ViewBag.Filter = filter;
            ViewBag.SortExpression = sortExpression;

            var roeQueryable = _context.RelOfficeResponsEmployee
                .Include(o => o.RoeOffice)
                .Include(r => r.RoeOffice.OfficeHouses.HousesRegion)
                .Include(e => e.RoeEmployee)
                .AsQueryable();
            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                roeQueryable = roeQueryable.Where(e => EF.Functions.Like(e.RoeOffice.OfficeName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.RoeEmployee.EmployeeFullFio.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.RoeOffice.OfficeHouses.HousesRegion.RegionName.ToUpper(), "%" + filter + "%")
                );
            }

            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync(roeQueryable, pageSize, page, sortExpression, "RoeId");

            model.RouteValue = new RouteValueDictionary {
                 { "filter", filter},
                { "sortExpression", sortExpression },
                { "page", page }
            };

            return View(model);
        }

        // GET: RelOfficeResponsEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relOfficeResponsEmployee = await _context.RelOfficeResponsEmployee
                .Include(r => r.RoeEmployee)
                .Include(r => r.RoeOffice)
                .FirstOrDefaultAsync(m => m.RoeId == id);
            if (relOfficeResponsEmployee == null)
            {
                return NotFound();
            }

            return View(relOfficeResponsEmployee);
        }

        // GET: RelOfficeResponsEmployees/Create
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var _Region = _context.Region.FirstOrDefault();
            var _House = _context.Houses.Where(h => h.HousesRegionId == _Region.RegionId).FirstOrDefault();

            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            ViewData["OfficeHousesId"] = _ControleItems.GetHousesByRegion(_Region.RegionId);
            ViewData["RoeEmployeeId"] = _ControleItems.GetEmployeesByRegion(_Region.RegionId);
            ViewData["RoeOfficeId"] = _ControleItems.GetOfficesByHouse(_House == null ? 0 : _House.HousesId);

            return View();
        }

        // POST: RelOfficeResponsEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoeId,RoeOfficeId,RoeEmployeeId")] RelOfficeResponsEmployee relOfficeResponsEmployee,
            string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (ModelState.IsValid)
            {
                _context.Add(relOfficeResponsEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        page = page,
                        filter = filter,
                        sortExpression = sortExpression
                    });
            }
            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName"
                ,relOfficeResponsEmployee.RoeOffice.OfficeHouses.HousesRegionId);
            ViewData["OfficeHousesId"] = new SelectList(_context.Houses, "HouseId", "HouseName"
                ,relOfficeResponsEmployee.RoeOffice.OfficeHousesId);
            ViewData["RoeEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFullFio"
                ,relOfficeResponsEmployee.RoeEmployeeId);
            ViewData["RoeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName"
                ,relOfficeResponsEmployee.RoeOfficeId);
            return View(relOfficeResponsEmployee);
        }

        // GET: RelOfficeResponsEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var relOfficeResponsEmployee = await _context.RelOfficeResponsEmployee.FindAsync(id);
            if (relOfficeResponsEmployee == null)
            {
                return NotFound();
            }

            
            var _relOR = _context.RelOfficeResponsEmployee
                .Include(h => h.RoeOffice.OfficeHouses)
                .Include(e => e.RoeEmployee)                
                .Include(o => o.RoeOffice)
                .AsNoTracking();

            _relOR = _relOR.Where(e => e.RoeId == id);

            var _region_id = _relOR.First().RoeOffice.OfficeHouses.HousesRegionId;

            var _offices = _context.Offices
                .Include(h => h.OfficeHouses)
                .AsNoTracking();
            _offices = _offices.Where(o => o.OfficeHousesId == _relOR.First().RoeOffice.OfficeHousesId);

            var _houses = _context.Houses
                .Include(h => h.HousesRegion)
                .AsNoTracking();
            _houses = _houses.Where(h => h.HousesRegionId == _region_id);

            var _employees = _context.Employees
                .Include(e => e.EmployeeRegion)
                .AsNoTracking();
            _employees = _employees.Where(e => e.EmployeeRegionId == _region_id);

            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", _region_id);
            ViewData["OfficeHousesId"] = new SelectList(_houses, "HousesId", "HousesName", _relOR.First().RoeOffice.OfficeHousesId);
            ViewData["RoeEmployeeId"] = new SelectList(_employees, "EmployeeId", "EmployeeFullFio"
                , relOfficeResponsEmployee.RoeEmployeeId);
            ViewData["RoeOfficeId"] = new SelectList(_offices, "OfficeId", "OfficeName"
                , relOfficeResponsEmployee.RoeOfficeId);

            return View(relOfficeResponsEmployee);
        }

        // POST: RelOfficeResponsEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoeId,RoeOfficeId,RoeEmployeeId")] RelOfficeResponsEmployee relOfficeResponsEmployee,
            string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id != relOfficeResponsEmployee.RoeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relOfficeResponsEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelOfficeResponsEmployeeExists(relOfficeResponsEmployee.RoeId))
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
                        page = page,
                        filter = filter,
                        sortExpression = sortExpression
                    });
            }

            var _offices = _context.Offices
                .Include(h => h.OfficeHouses)
                .AsNoTracking();
            _offices = _offices.Where(o => o.OfficeId == relOfficeResponsEmployee.RoeOfficeId);

            var _houses = _context.Houses
                .Include(h => h.HousesRegion)
                .AsNoTracking();
            _houses = _houses.Where(h => h.HousesRegionId == relOfficeResponsEmployee.RoeOffice.OfficeHouses.HousesRegionId);

            var _employees = _context.Employees
                .Include(e => e.EmployeeRegion)
                .AsNoTracking();
            _employees = _employees.Where(e => e.EmployeeRegionId == relOfficeResponsEmployee.RoeOffice.OfficeHouses.HousesRegionId);

            ViewData["OfficeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName"
                , relOfficeResponsEmployee.RoeOffice.OfficeHouses.HousesRegionId);
            ViewData["OfficeHousesId"] = new SelectList(_houses, "HouseId", "HouseName"
                , relOfficeResponsEmployee.RoeOffice.OfficeHousesId);

            ViewData["RoeEmployeeId"] = new SelectList(_employees, "EmployeeId", "EmployeeFullFio"
                , relOfficeResponsEmployee.RoeEmployeeId);
            ViewData["RoeOfficeId"] = new SelectList(_offices, "OfficeId", "OfficeName"
                , relOfficeResponsEmployee.RoeOfficeId);
            return View(relOfficeResponsEmployee);
        }

        // GET: RelOfficeResponsEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var relOfficeResponsEmployee = await _context.RelOfficeResponsEmployee
                .Include(r => r.RoeEmployee)
                .Include(r => r.RoeOffice)
                .FirstOrDefaultAsync(m => m.RoeId == id);
            if (relOfficeResponsEmployee == null)
            {
                return NotFound();
            }

            return View(relOfficeResponsEmployee);
        }

        // POST: RelOfficeResponsEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var relOfficeResponsEmployee = await _context.RelOfficeResponsEmployee.FindAsync(id);
            _context.RelOfficeResponsEmployee.Remove(relOfficeResponsEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),
                new
                {
                    page = page,
                    filter = filter,
                    sortExpression = sortExpression
                });
        }

        private bool RelOfficeResponsEmployeeExists(int id)
        {
            return _context.RelOfficeResponsEmployee.Any(e => e.RoeId == id);
        }
    }
}
