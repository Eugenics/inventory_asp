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
                { "filter", filter}
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
            ViewData["RoeEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname");
            ViewData["RoeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName");
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
            if (ModelState.IsValid)
            {
                _context.Add(relOfficeResponsEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoeEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname", relOfficeResponsEmployee.RoeEmployeeId);
            ViewData["RoeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", relOfficeResponsEmployee.RoeOfficeId);
            return View(relOfficeResponsEmployee);
        }

        // GET: RelOfficeResponsEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            if (id == null)
            {
                return NotFound();
            }

            var relOfficeResponsEmployee = await _context.RelOfficeResponsEmployee.FindAsync(id);
            if (relOfficeResponsEmployee == null)
            {
                return NotFound();
            }
            ViewData["RoeEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname", relOfficeResponsEmployee.RoeEmployeeId);
            ViewData["RoeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", relOfficeResponsEmployee.RoeOfficeId);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoeEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname", relOfficeResponsEmployee.RoeEmployeeId);
            ViewData["RoeOfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", relOfficeResponsEmployee.RoeOfficeId);
            return View(relOfficeResponsEmployee);
        }

        // GET: RelOfficeResponsEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "RoeId")
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

        // POST: RelOfficeResponsEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filter = "", int page = 1, string sortExpression = "RoeId")
        {
            var relOfficeResponsEmployee = await _context.RelOfficeResponsEmployee.FindAsync(id);
            _context.RelOfficeResponsEmployee.Remove(relOfficeResponsEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelOfficeResponsEmployeeExists(int id)
        {
            return _context.RelOfficeResponsEmployee.Any(e => e.RoeId == id);
        }
    }
}
