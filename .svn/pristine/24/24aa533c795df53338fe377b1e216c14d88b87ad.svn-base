using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Authorization;
//using inventory_dot_core.Classes.Paging;
using Microsoft.AspNetCore.Routing;

namespace inventory_dot_core.Views
{
    [Authorize(Policy = "RefEditorsRole")]
    public class EmployeesController : Controller
    {
        private readonly InventoryContext _context;

        private Classes.ControlesItems _ControleItems;

        public EmployeesController(InventoryContext context)
        {
            _context = context;
            _ControleItems = new Classes.ControlesItems(_context);
        }

        // GET: Employees
        //public async Task<IActionResult> Index()
        //{
        //    var inventoryContext = _context.Employees.Include(e => e.EmployeeOffice).Include(e => e.EmployeePosition).Include(e => e.EmployeeRegion);
        //    return View(await inventoryContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "EmployeeId")
        {
            ViewBag.Filter = filter;
            ViewBag.SortExpression = sortExpression;

            var employeesQueryable = _context.Employees
                .Include(e => e.EmployeeOffice)
                .Include(p => p.EmployeePosition)
                .Include(r => r.EmployeeRegion)                
                .AsQueryable();
            int pageSize = 10;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                employeesQueryable = employeesQueryable.Where(e => EF.Functions.Like(e.EmployeeFullFio.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.EmployeeOffice.OfficeName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.EmployeeRegion.RegionName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.EmployeePosition.PositionName.ToUpper(), "%" + filter + "%")
                );
            }

            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync(employeesQueryable, pageSize, page, sortExpression, "EmployeeId");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.EmployeeOffice)
                .Include(e => e.EmployeePosition)
                .Include(e => e.EmployeeRegion)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        [HttpGet]
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "EmployeeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            ViewData["EmployeeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");

            var _Region = _context.Region.FirstOrDefault();

            ViewData["EmployeePositionId"] = _ControleItems.GetPositionsByRegion(_Region.RegionId);
            ViewData["EmployeeOfficeId"] = _ControleItems.GetOfficesByRegion(_Region.RegionId);

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(
            "EmployeeId," +
            "EmployeeFirstname,EmployeeLastname,EmployeeMiddlename," +
            "EmployeeEmail,EmployeePositionId,EmployeeOfficeId,EmployeePhoneWork," +
            "EmployeeNote,EmployeeFullFio,EmployeeIsRespons,EmployeeIsMol,EmployeeRegionId")] Employees employees,
            bool EmployeeIsRespons = false,
            bool EmployeeIsMol = false,
            string filter = "", int page = 1, string sortExpression = "EmployeeId")
        {
            employees.EmployeeIsRespons = EmployeeIsRespons ? 1 : 0;
            employees.EmployeeIsMol = EmployeeIsMol ? 1 : 0;
            employees.EmployeeFullFio =
                    $"{employees.EmployeeLastname} {employees.EmployeeFirstname} {employees.EmployeeMiddlename}";

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }
            ViewData["EmployeeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", employees.EmployeeOfficeId);
            ViewData["EmployeePositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", employees.EmployeePositionId);
            ViewData["EmployeeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", employees.EmployeeRegionId);
            return View(employees);
        }

        // GET: Employees/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "EmployeeId")
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            //ViewData["EmployeeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", employees.EmployeeOfficeId);
            //ViewData["EmployeePositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", employees.EmployeePositionId);
            ViewData["EmployeeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", employees.EmployeeRegionId);

            ViewData["EmployeePositionId"] = _ControleItems.GetPositionsByRegion(employees.EmployeeRegionId);
            ViewData["EmployeeOfficeId"] = _ControleItems.GetOfficesByRegion(employees.EmployeeRegionId);

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(
                "EmployeeId,EmployeeFirstname,EmployeeLastname,EmployeeMiddlename," +
                "EmployeeEmail,EmployeePositionId,EmployeeOfficeId,EmployeePhoneWork," +
                "EmployeeNote,EmployeeFullFio,EmployeeIsRespons,EmployeeIsMol,EmployeeRegionId")] Employees employees,
            bool EmployeeIsRespons = false,
            bool EmployeeIsMol = false,
            string filter = "", int page = 1, string sortExpression = "EmployeeId")
        {
            if (id != employees.EmployeeId)
            {
                return NotFound();
            }

            employees.EmployeeIsRespons = EmployeeIsRespons ? 1 : 0;
            employees.EmployeeIsMol = EmployeeIsMol ? 1 : 0;
            employees.EmployeeFullFio =
                    $"{employees.EmployeeLastname} {employees.EmployeeFirstname} {employees.EmployeeMiddlename}";

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                try
                {                    
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeId))
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
            ViewData["EmployeeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", employees.EmployeeOfficeId);
            ViewData["EmployeePositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", employees.EmployeePositionId);
            ViewData["EmployeeRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", employees.EmployeeRegionId);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "EmployeeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.EmployeeOffice)
                .Include(e => e.EmployeePosition)
                .Include(e => e.EmployeeRegion)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        } 
        
        [HttpGet]
        public ActionResult GetJSONPositions(int regonId)
        {
            return Json(_ControleItems.GetPositionsByRegion(regonId));
        }

        [HttpGet]
        public ActionResult GetJSONOffices(int regionId)
        {
            return Json(_ControleItems.GetOfficesByRegion(regionId));
        }
    }
}
