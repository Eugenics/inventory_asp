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
using inventory_dot_core.Classes;
using Microsoft.AspNetCore.Identity;

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
    public class DepartmentsController : Controller
    {
        private readonly InventoryContext _context;
        private ControlesItems _ControleItems;
        private readonly UserManager<ApplicationUser> _userManager;

        public DepartmentsController(InventoryContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _ControleItems = new Classes.ControlesItems(_context);
            _userManager = userManager;
        }

        // GET: Departments
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "DepartmentId")
        {
            ViewBag.Filter = filter;
            ViewBag.SortExpression = sortExpression;

            var departmentsQueryable = _context.Departments
                .Include(d => d.DepartmentParent)
                .Include(d => d.DepartmentRegion)
                .AsQueryable();

            int pageSize = 15;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                departmentsQueryable = departmentsQueryable.Where(e => EF.Functions.Like(e.DepartmentName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.DepartmentParent.DepartmentName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.DepartmentRegion.RegionName.ToUpper(), "%" + filter + "%")
                );
            }

            // Filter regions in dataset according user rights
            RegionFilter rFilter = new RegionFilter();
            departmentsQueryable = rFilter.SetRegionFilter(
                departmentsQueryable,
                _userManager,
                HttpContext,
                "DepartmentRegionId"
                );

            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                (
                   departmentsQueryable, pageSize, page, sortExpression, "DepartmentId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "sortExpression", sortExpression },
                { "page", page }
            };

            return View(model);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Include(d => d.DepartmentParent)
                .Include(d => d.DepartmentRegion)
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: Departments/Create
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "DepartmentId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var _Region = _context.Region.FirstOrDefault();

            ViewData["DepartmentParentId"] = _ControleItems.GetDepartmentsByRegion(_Region.RegionId);
            ViewData["DepartmentRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(
            "DepartmentId,DepartmentName,DepartmentNotes,DepartmentRegionId,DepartmentParentId")] Departments departments,
            string filter = "", int page = 1, string sortExpression = "DepartmentId")
        {
            if (ModelState.IsValid)
            {
                _context.Add(departments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }
            ViewData["DepartmentParentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", departments.DepartmentParentId);
            ViewData["DepartmentRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", departments.DepartmentRegionId);
            return View(departments);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "DepartmentId")
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var departments = await _context.Departments.FindAsync(id);
            if (departments == null)
            {
                return NotFound();
            }
            ViewData["DepartmentParentId"] = _ControleItems.GetDepartmentsByRegion(departments.DepartmentRegionId);
            ViewData["DepartmentRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", departments.DepartmentRegionId);
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(
            "DepartmentId,DepartmentName,DepartmentNotes,DepartmentRegionId,DepartmentParentId")] Departments departments,
            string filter = "", int page = 1, string sortExpression = "DepartmentId")
        {
            if (id != departments.DepartmentId)
            {
                return NotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(departments.DepartmentId))
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
            ViewData["DepartmentParentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", departments.DepartmentParentId);
            ViewData["DepartmentRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", departments.DepartmentRegionId);
            return View(departments);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "DepartmentId")
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var departments = await _context.Departments
                .Include(d => d.DepartmentParent)
                .Include(d => d.DepartmentRegion)
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filter = "", int page = 1, string sortExpression = "DepartmentId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var departments = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(departments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),
                new
                {
                    filter = filter,
                    page = page,
                    sortExpression = sortExpression
                });
        }

        private bool DepartmentsExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
