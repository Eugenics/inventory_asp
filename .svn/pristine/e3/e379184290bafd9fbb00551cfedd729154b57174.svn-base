using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Authorization;
using inventory_dot_core.Classes;
using Microsoft.AspNetCore.Routing;

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
    public class PositionsController : Controller
    {
        private readonly InventoryContext _context;
        private ControlesItems _ControleItems;

        public PositionsController(InventoryContext context)
        {
            _context = context;
            _ControleItems = new ControlesItems(_context);
        }

        // GET: Positions
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "PositionId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
                        
            var positionsQry = _context.Positions
                .Include(p => p.PositionDepartment)
                .Include(r => r.PositionDepartment.DepartmentRegion)
                .AsQueryable();

            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                positionsQry = positionsQry.Where(e => EF.Functions.Like(e.PositionName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.PositionDepartment.DepartmentName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.PositionDepartment.DepartmentRegion.RegionName.ToUpper(), "%" + filter + "%")
                );
            }
            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                (
                   positionsQry, pageSize, page, sortExpression, "PositionId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positions = await _context.Positions
                .Include(p => p.PositionDepartment)
                .FirstOrDefaultAsync(m => m.PositionId == id);
            if (positions == null)
            {
                return NotFound();
            }

            return View(positions);
        }

        // GET: Positions/Create
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "PositionId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var _Region = _context.Region.FirstOrDefault();

            ViewData["PositionDepartmentId"] = _ControleItems.GetDepartmentsByRegion(_Region.RegionId);
            ViewData["PositionRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionId,PositionName,PositionNotes,PositionDepartmentId")] Positions positions,
            string filter = "", int page = 1, string sortExpression = "PositionId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (ModelState.IsValid)
            {
                _context.Add(positions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }

            ViewData["PositionDepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", positions.PositionDepartmentId);
            ViewData["PositionRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View(positions);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "PositionId")
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var positions = await _context.Positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }

            var _positions = _context.Positions.Include(p => p.PositionDepartment.DepartmentRegion).AsNoTracking();

            _positions = _positions.Where(p => p.PositionId == id);

            ViewData["PositionDepartmentId"] = _ControleItems.GetDepartmentsByRegion(_positions.First().PositionDepartment.DepartmentRegion.RegionId);
            ViewData["PositionRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View(positions);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PositionId,PositionName,PositionNotes,PositionDepartmentId")] Positions positions,
            string filter = "", int page = 1, string sortExpression = "PositionId")
        {
            if (id != positions.PositionId)
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
                    _context.Update(positions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionsExists(positions.PositionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)
                    , new { filter = filter, page = page, sortExpression = sortExpression });
            }
            ViewData["PositionDepartmentId"] = _ControleItems.GetPositionsByRegion(positions.PositionDepartment.DepartmentRegionId);
            ViewData["PositionRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View(positions);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "PositionId")
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var positions = await _context.Positions
                .Include(p => p.PositionDepartment)
                .FirstOrDefaultAsync(m => m.PositionId == id);
            if (positions == null)
            {
                return NotFound();
            }

            return View(positions);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filter = "", int page = 1, string sortExpression = "PositionId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var positions = await _context.Positions.FindAsync(id);
            _context.Positions.Remove(positions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)
                , new { filter = filter, page = page, sortExpression = sortExpression });
        }

        private bool PositionsExists(int id)
        {
            return _context.Positions.Any(e => e.PositionId == id);
        }
    }
}
