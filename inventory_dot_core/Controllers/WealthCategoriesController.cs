using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Routing;

namespace inventory_dot_core.Views
{
    public class WealthCategoriesController : Controller
    {
        private readonly InventoryContext _context;

        public WealthCategoriesController(InventoryContext context)
        {
            _context = context;
        }

        // GET: WealthCategories
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "WcatId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var catQry = _context.WealthCategories.AsQueryable();

            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                catQry = catQry.Where(e => EF.Functions.Like(e.Wcatname.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.Wcatnotes.ToUpper(), "%" + filter + "%")
                );
            }
            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                (
                   catQry, pageSize, page, sortExpression, "WtypeId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: WealthCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wealthCategories = await _context.WealthCategories
                .FirstOrDefaultAsync(m => m.WcatId == id);
            if (wealthCategories == null)
            {
                return NotFound();
            }

            return View(wealthCategories);
        }

        // GET: WealthCategories/Create
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "WcatId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            return View();
        }

        // POST: WealthCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WcatId,Wcatname,Wcatnotes")] WealthCategories wealthCategories,
            string filter = "", int page = 1, string sortExpression = "WcatId")
        {

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (ModelState.IsValid)
            {
                _context.Add(wealthCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wealthCategories);
        }

        // GET: WealthCategories/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "WcatId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var wealthCategories = await _context.WealthCategories.FindAsync(id);
            if (wealthCategories == null)
            {
                return NotFound();
            }
            return View(wealthCategories);
        }

        // POST: WealthCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WcatId,Wcatname,Wcatnotes")] WealthCategories wealthCategories,
            string filter = "", int page = 1, string sortExpression = "WcatId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id != wealthCategories.WcatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wealthCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WealthCategoriesExists(wealthCategories.WcatId))
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
            return View(wealthCategories);
        }

        // GET: WealthCategories/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "WcatId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var wealthCategories = await _context.WealthCategories
                .FirstOrDefaultAsync(m => m.WcatId == id);
            if (wealthCategories == null)
            {
                return NotFound();
            }

            return View(wealthCategories);
        }

        // POST: WealthCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filter = "", int page = 1, string sortExpression = "WcatId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var wealthCategories = await _context.WealthCategories.FindAsync(id);
            _context.WealthCategories.Remove(wealthCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WealthCategoriesExists(int id)
        {
            return _context.WealthCategories.Any(e => e.WcatId == id);
        }
    }
}
