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
    public class RelSoftwareHardwaresController : Controller
    {
        private readonly InventoryContext _context;
        private static string _hard_filter;
        private static int _hard_page;
        private static string _hard_sortExpression;
        //private static int indexPageOpnCnt = 0;
        private static int _hardware_id;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RelSoftwareHardwaresController(InventoryContext context)
        {
            _context = context;
        }

        // GET: RelSoftwareHardwares
        public async Task<IActionResult> Index(int hardware_id, string filter = "",
            int page = 1,
            int hard_page = 1,
            string hard_filter = "",
            string filterInv = "",
            string filterCat = "",
            string filterType = "",
            string filterName = "",
            string filterOffice = "",
            string filterRegion = "",
            string sortExpression = "RelshId",
            string hard_sortExpression = "WhardId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.HardwareId = hardware_id;
            ViewBag.FilterInv = filterInv;
            ViewBag.FilterName = filterName;
            ViewBag.FilterRegion = filterRegion;
            ViewBag.FilterCat = filterCat;
            ViewBag.FilterType = filterType;
            ViewBag.FilterOffice = filterOffice;

            // Открыли страницу от другого сотрудника - сохраняем параметры для перехода к сотрудникам
            if (_hardware_id != hardware_id)
            {
                _hardware_id = hardware_id;
                _hard_filter = hard_filter;
                _hard_page = hard_page;
                _hard_sortExpression = hard_sortExpression;
            }

            ViewBag.HardFilter = _hard_filter;
            ViewBag.HardPage = _hard_page;
            ViewBag.HardSortExpression = _hard_sortExpression;

            var inventoryContext = _context.RelSoftwareHardware
                .Include(r => r.RelshWhard)
                .Include(r => r.RelshWsoft)
                .Where(r => r.RelshWhardId == hardware_id)
                .AsQueryable();

            var _hardware = _context.WealthHardware.Find(hardware_id);

            ViewBag.HardwareName = _hardware.WhardName;

            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                inventoryContext = inventoryContext.Where(
                    e => EF.Functions.Like(e.RelshWhard.WhardName.ToUpper(), "%" + filter + "%")
                );
            }
            var model = await Classes.Paging.PagingList.CreateAsync
                (
                   inventoryContext, pageSize, page, sortExpression, "RelshId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "HardwareId", hardware_id},
                { "FilterInv", filterInv},
                { "filterOffice", filterOffice},
                { "filterName", filterName},
                { "filterCat", filterCat},
                { "filterType", filterType},
                { "filterRegion", filterRegion}
            };

            return View(model);
        }

        // GET: RelSoftwareHardwares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relSoftwareHardware = await _context.RelSoftwareHardware
                .Include(r => r.RelshWhard)
                .Include(r => r.RelshWsoft)
                .FirstOrDefaultAsync(m => m.RelshId == id);

            if (relSoftwareHardware == null)
            {
                return NotFound();
            }

            return View(relSoftwareHardware);
        }

        // GET: RelSoftwareHardwares/Create
        public IActionResult Create(int hardware_id, string filter = "", int page = 1, string sortExpression = "RelshId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.HardwareId = hardware_id;

            var _hardware = _context.WealthHardware.Find(hardware_id);

            ViewBag.HardwareName = _hardware.WhardName;

            ViewData["RelshIdHardwareId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardName")
                .Where(e => e.Value == hardware_id.ToString());
            ViewData["RelshWsoftId"] = GetNotUseSoftList(0, _hardware.WhardRegionId.Value);

            return View();
        }

        // POST: RelSoftwareHardwares/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelshId,RelshWsoftId,RelshWhardId")] RelSoftwareHardware relSoftwareHardware,
            int hardware_id, string filter = "", int page = 1, string sortExpression = "RelshId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;

            var _hardware = _context.WealthHardware.Find(hardware_id);

            ViewBag.EmployeeName = _hardware.WhardName;

            relSoftwareHardware.RelshWhardId = hardware_id;

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                _context.Add(relSoftwareHardware);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        hardware_id,
                        filter,
                        page,
                        sortExpression
                    });
            }

            ViewData["RelshIdHardwareId"] = new SelectList(_context.Employees, "WhardId", "WhardName")
                .Where(e => e.Value == hardware_id.ToString());
            ViewData["RelshIdSoftId"] = new SelectList(
                GetNotUseSoftList(0, relSoftwareHardware.RelshWhard.WhardRegionId.Value),
                "WhardId", "WhardName");

            return View();
        }

        // GET: RelSoftwareHardwares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relSoftwareHardware = await _context.RelSoftwareHardware.FindAsync(id);
            if (relSoftwareHardware == null)
            {
                return NotFound();
            }

            ViewData["RelshWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", relSoftwareHardware.RelshWhardId);
            ViewData["RelshWsoftId"] = new SelectList(_context.WealthSoftware, "WsoftId", "WsoftName", relSoftwareHardware.RelshWsoftId);

            return View(relSoftwareHardware);
        }

        // POST: RelSoftwareHardwares/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RelshId,RelshWsoftId,RelshWhardId")] RelSoftwareHardware relSoftwareHardware)
        {
            if (id != relSoftwareHardware.RelshId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relSoftwareHardware);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelSoftwareHardwareExists(relSoftwareHardware.RelshId))
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
            ViewData["RelshWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", relSoftwareHardware.RelshWhardId);
            ViewData["RelshWsoftId"] = new SelectList(_context.WealthSoftware, "WsoftId", "WsoftName", relSoftwareHardware.RelshWsoftId);

            return View(relSoftwareHardware);
        }

        // GET: RelSoftwareHardwares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relSoftwareHardware = await _context.RelSoftwareHardware
                .Include(r => r.RelshWhard)
                .Include(r => r.RelshWsoft)
                .FirstOrDefaultAsync(m => m.RelshId == id);

            if (relSoftwareHardware == null)
            {
                return NotFound();
            }

            return View(relSoftwareHardware);
        }

        // POST: RelSoftwareHardwares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relSoftwareHardware = await _context.RelSoftwareHardware.FindAsync(id);
            _context.RelSoftwareHardware.Remove(relSoftwareHardware);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool RelSoftwareHardwareExists(int id)
        {
            return _context.RelSoftwareHardware.Any(e => e.RelshId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curWhardId"></param>
        /// <param name="regionId"></param>
        /// <param name="officeId"></param>
        /// <returns></returns>
        private List<SelectListItem> GetNotUseSoftList(int curSoftdId = 0, int regionId = 0)
        {
            // Создаем список используемого ПО
            var _softwareInUse = _context.RelSoftwareHardware.Select(h => h.RelshWsoftId).ToArray();

            // Удаляем из списка используемого ПО текущее. Это необходимо для списка при редактировании.
            if (curSoftdId != 0)
            {
                _softwareInUse = _softwareInUse.Where(l => l != curSoftdId).ToArray();
            }

            // Получаем список не используемого ПО
            var _hardwareNotInUse = _context.WealthSoftware
                .Where(h => !_softwareInUse.Contains(h.WsoftId)
                && h.WsoftRegionId == regionId
                )
                .OrderBy(h => h.WsoftInumber);

            var retList = new List<SelectListItem>();

            foreach (var h in _hardwareNotInUse)
            {
                retList.Add(new SelectListItem(h.WsoftInumber + " | " + h.WsoftName, h.WsoftId.ToString(), false, false));
            }

            return retList;
        }
    }
}
