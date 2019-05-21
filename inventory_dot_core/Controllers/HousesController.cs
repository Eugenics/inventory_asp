using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;

namespace inventory_dot_core.Controllers
{
    public class HousesController : Controller
    {
        private readonly inventoryContext _context;

        public HousesController(inventoryContext context)
        {
            _context = context;
        }

        // GET: Houses
        public async Task<IActionResult> Index()
        {
            var inventoryContext = _context.Houses.Include(h => h.HousesRegion);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: Houses/Details/5
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
        public IActionResult Create()
        {
            ViewData["HousesRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HousesId,HousesName,HousesRem,HousesRegionId")] Houses houses)
        {
            if (id != houses.HousesId)
            {
                return NotFound();
            }

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
            ViewData["HousesRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", houses.HousesRegionId);
            return View(houses);
        }

        // GET: Houses/Delete/5
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
    }
}
