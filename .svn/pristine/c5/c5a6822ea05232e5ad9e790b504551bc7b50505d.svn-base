using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Routing;
using inventory_dot_core.Classes;
using Microsoft.AspNetCore.Authorization;

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
    public class WealthSoftwaresController : Controller
    {
        private readonly InventoryContext _context;

        private ControlesItems _ControlesItems;


        public WealthSoftwaresController(InventoryContext context)
        {
            _context = context;
            _ControlesItems = new ControlesItems(_context);
        }

        // GET: WealthSoftwares
        public async Task<IActionResult> Index(string filter = "", int page = 1, string sortExpression = "WsoftId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var inventoryContext = _context.WealthSoftware.Include(w => w.WsoftRegion)
                .Include(w => w.WsoftWcat)
                .Include(w => w.WsoftWtype)
                .AsQueryable();

            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                inventoryContext = inventoryContext.Where(e => EF.Functions.Like(e.WsoftFnumber.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.WsoftInumber.ToUpper(), "%" + filter + "%")
                    || EF.Functions.Like(e.WsoftName.ToUpper(), "%" + filter + "%")                    
                    || EF.Functions.Like(e.WsoftRegion.RegionName.ToUpper(), "%" + filter + "%")
                );
            }
            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                (
                   inventoryContext, pageSize, page, sortExpression, "WsoftId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };


            return View(model);
        }

        // GET: WealthSoftwares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wealthSoftware = await _context.WealthSoftware
                .Include(w => w.WsoftRegion)
                .Include(w => w.WsoftWcat)
                .Include(w => w.WsoftWtype)
                .FirstOrDefaultAsync(m => m.WsoftId == id);
            if (wealthSoftware == null)
            {
                return NotFound();
            }

            return View(wealthSoftware);
        }

        // GET: WealthSoftwares/Create
        public IActionResult Create(string filter = "", int page = 1, string sortExpression = "WsoftId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            ViewData["WsoftRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            ViewData["WsoftWcatId"] = new SelectList(_context.WealthCategories, "WcatId", "Wcatname");
            ViewData["WsoftWtypeId"] = new SelectList(_context.WealthTypes, "WtypeId", "WtypeName")
                .Where(t => t.Value == "105"
                    || t.Value == "102");
            return View();
        }

        // POST: WealthSoftwares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WsoftId,WsoftInumber,WsoftFnumber,WsoftWcatId,WsoftWtypeId,WsoftName" +
            ",WsoftDateOfAdoption,WsoftInitialCost,WsoftResidualValue,WsoftRegionId" +
            ",WsoftNote,WsoftArchiv,WsoftCreateDate,WsoftCnt")] WealthSoftware wealthSoftware,
            bool WsoftArchiv,
            string filter = "", int page = 1, string sortExpression = "WsoftId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            wealthSoftware.WsoftArchiv = WsoftArchiv ? 1 : 0;

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                _context.Add(wealthSoftware);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }
            ViewData["WsoftRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", wealthSoftware.WsoftRegionId);
            ViewData["WsoftWcatId"] = new SelectList(_context.WealthCategories, "WcatId", "Wcatname", wealthSoftware.WsoftWcatId);
            ViewData["WsoftWtypeId"] = new SelectList(_context.WealthTypes, "WtypeId", "WtypeName", wealthSoftware.WsoftWtypeId)
                .Where(t => t.Value == "105"
                    || t.Value == "102");
            return View(wealthSoftware);
        }

        // GET: WealthSoftwares/Edit/5
        public async Task<IActionResult> Edit(int? id, string filter = "", int page = 1, string sortExpression = "WsoftId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var wealthSoftware = await _context.WealthSoftware.FindAsync(id);
            if (wealthSoftware == null)
            {
                return NotFound();
            }
            ViewData["WsoftRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", wealthSoftware.WsoftRegionId);
            ViewData["WsoftWcatId"] = new SelectList(_context.WealthCategories, "WcatId", "Wcatname", wealthSoftware.WsoftWcatId);
            ViewData["WsoftWtypeId"] = new SelectList(_context.WealthTypes, "WtypeId", "WtypeName", wealthSoftware.WsoftWtypeId)
                .Where(t => t.Value == "105"
                    || t.Value == "102");
            return View(wealthSoftware);
        }

        // POST: WealthSoftwares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WsoftId,WsoftInumber,WsoftFnumber,WsoftWcatId,WsoftWtypeId" +
            ",WsoftName,WsoftDateOfAdoption,WsoftInitialCost,WsoftResidualValue,WsoftRegionId" +
            ",WsoftNote,WsoftArchiv,WsoftCreateDate,WsoftCnt")] WealthSoftware wealthSoftware,
            bool WsoftArchiv,
            string filter = "", int page = 1, string sortExpression = "WsoftId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            wealthSoftware.WsoftArchiv = WsoftArchiv ? 1 : 0;

            ModelState.Clear();

            if (id != wealthSoftware.WsoftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wealthSoftware);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WealthSoftwareExists(wealthSoftware.WsoftId))
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
            ViewData["WsoftRegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", wealthSoftware.WsoftRegionId);
            ViewData["WsoftWcatId"] = new SelectList(_context.WealthCategories, "WcatId", "Wcatname", wealthSoftware.WsoftWcatId);
            ViewData["WsoftWtypeId"] = new SelectList(_context.WealthTypes, "WtypeId", "WtypeName", wealthSoftware.WsoftWtypeId)
                .Where(t => t.Value == "105"
                    || t.Value == "102");
            return View(wealthSoftware);
        }

        // GET: WealthSoftwares/Delete/5
        public async Task<IActionResult> Delete(int? id, string filter = "", int page = 1, string sortExpression = "WsoftId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            if (id == null)
            {
                return NotFound();
            }

            var wealthSoftware = await _context.WealthSoftware
                .Include(w => w.WsoftRegion)
                .Include(w => w.WsoftWcat)
                .Include(w => w.WsoftWtype)
                .FirstOrDefaultAsync(m => m.WsoftId == id);
            if (wealthSoftware == null)
            {
                return NotFound();
            }

            return View(wealthSoftware);
        }

        // POST: WealthSoftwares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string filter = "", int page = 1, string sortExpression = "WsoftId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var wealthSoftware = await _context.WealthSoftware.FindAsync(id);
            _context.WealthSoftware.Remove(wealthSoftware);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),
                new
                {
                    filter = filter,
                    page = page,
                    sortExpression = sortExpression
                });
        }

        private bool WealthSoftwareExists(int id)
        {
            return _context.WealthSoftware.Any(e => e.WsoftId == id);
        }
    }
}
