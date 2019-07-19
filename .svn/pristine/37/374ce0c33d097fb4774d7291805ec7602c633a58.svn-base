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
    public class AccountingBatteriesController : Controller
    {
        private readonly InventoryContext _context;

        public AccountingBatteriesController(InventoryContext context)
        {
            _context = context;
        }

        // GET: AccountingBatteries
        public async Task<IActionResult> Index()
        {
            var inventoryContext = _context.AccountingBatteries.Include(a => a.AbBatWhard).Include(a => a.AbWhard);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: AccountingBatteries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountingBatteries = await _context.AccountingBatteries
                .Include(a => a.AbBatWhard)
                .Include(a => a.AbWhard)
                .FirstOrDefaultAsync(m => m.AbId == id);
            if (accountingBatteries == null)
            {
                return NotFound();
            }

            return View(accountingBatteries);
        }

        // GET: AccountingBatteries/Create
        public IActionResult Create()
        {
            ViewData["AbBatWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId");
            ViewData["AbWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId");
            return View();
        }

        // POST: AccountingBatteries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AbId,AbBatWhardId,AbWhardId")] AccountingBatteries accountingBatteries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountingBatteries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbBatWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", accountingBatteries.AbBatWhardId);
            ViewData["AbWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", accountingBatteries.AbWhardId);
            return View(accountingBatteries);
        }

        // GET: AccountingBatteries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountingBatteries = await _context.AccountingBatteries.FindAsync(id);
            if (accountingBatteries == null)
            {
                return NotFound();
            }
            ViewData["AbBatWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", accountingBatteries.AbBatWhardId);
            ViewData["AbWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", accountingBatteries.AbWhardId);
            return View(accountingBatteries);
        }

        // POST: AccountingBatteries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AbId,AbBatWhardId,AbWhardId")] AccountingBatteries accountingBatteries)
        {
            if (id != accountingBatteries.AbId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountingBatteries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountingBatteriesExists(accountingBatteries.AbId))
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
            ViewData["AbBatWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", accountingBatteries.AbBatWhardId);
            ViewData["AbWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", accountingBatteries.AbWhardId);
            return View(accountingBatteries);
        }

        // GET: AccountingBatteries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountingBatteries = await _context.AccountingBatteries
                .Include(a => a.AbBatWhard)
                .Include(a => a.AbWhard)
                .FirstOrDefaultAsync(m => m.AbId == id);
            if (accountingBatteries == null)
            {
                return NotFound();
            }

            return View(accountingBatteries);
        }

        // POST: AccountingBatteries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountingBatteries = await _context.AccountingBatteries.FindAsync(id);
            _context.AccountingBatteries.Remove(accountingBatteries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountingBatteriesExists(int id)
        {
            return _context.AccountingBatteries.Any(e => e.AbId == id);
        }
    }
}
