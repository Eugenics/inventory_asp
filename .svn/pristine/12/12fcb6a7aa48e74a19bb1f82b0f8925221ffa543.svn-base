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

namespace inventory_dot_core.Views
{
    [Authorize(Policy = "RefEditorsRole")]
    public class WealthTypesController : Controller
    {
        private readonly InventoryContext _context;

        public WealthTypesController(InventoryContext context)
        {
            _context = context;
        }

        // GET: WealthTypes
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "WtypeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var typesQry = _context.WealthTypes.AsQueryable();

            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                typesQry = typesQry.Where(e => EF.Functions.Like(e.WtypeName.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.WtypeNotes.ToUpper(), "%" + filter + "%")
                );
            }
            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                (
                   typesQry, pageSize, page, sortExpression, "WtypeId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: WealthTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wealthTypes = await _context.WealthTypes
                .FirstOrDefaultAsync(m => m.WtypeId == id);
            if (wealthTypes == null)
            {
                return NotFound();
            }

            return View(wealthTypes);
        }

        // GET: WealthTypes/Create
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "WtypeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            return View();
        }

        // POST: WealthTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WtypeId,WtypeName,WtypeNotes")] WealthTypes wealthTypes,
            string filter = "", int page = 1, string sortExpression = "WtypeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (ModelState.IsValid)
            {
                _context.Add(wealthTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }
            return View(wealthTypes);
        }

        // GET: WealthTypes/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "WtypeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var wealthTypes = await _context.WealthTypes.FindAsync(id);
            if (wealthTypes == null)
            {
                return NotFound();
            }
            return View(wealthTypes);
        }

        // POST: WealthTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WtypeId,WtypeName,WtypeNotes")] WealthTypes wealthTypes,
            string filter = "", int page = 1, string sortExpression = "WtypeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id != wealthTypes.WtypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wealthTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WealthTypesExists(wealthTypes.WtypeId))
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
            return View(wealthTypes);
        }

        // GET: WealthTypes/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "WtypeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var wealthTypes = await _context.WealthTypes
                .FirstOrDefaultAsync(m => m.WtypeId == id);
            if (wealthTypes == null)
            {
                return NotFound();
            }

            return View(wealthTypes);
        }

        // POST: WealthTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filter = "", int page = 1, string sortExpression = "WtypeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var wealthTypes = await _context.WealthTypes.FindAsync(id);
            _context.WealthTypes.Remove(wealthTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
        }

        private bool WealthTypesExists(int id)
        {
            return _context.WealthTypes.Any(e => e.WtypeId == id);
        }
    }
}
