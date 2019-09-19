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
    public class RelHardwareSoftwaresController : Controller
    {
        private readonly InventoryContext _context;

        private static int _soft_id;
        private static string _soft_filter;
        private static int _soft_page;
        private static string _soft_sortExpression;

        public RelHardwareSoftwaresController(InventoryContext context)
        {
            _context = context;
        }

        // GET: RelHardwareSoftwares
        public async Task<IActionResult> Index(
            int soft_id, 
            string filter = "", 
            int page = 1, 
            string sortExpression = "RelshId",
            string filterInv = "",                        
            string filterName = "",            
            string filterRegion = "",
            int soft_page = 1, 
            string soft_sortExpression = "")
        {
            ViewBag.FilterInv = filterInv;
            ViewBag.FilterName = filterName;
            ViewBag.FilterRegion = filterRegion;

            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.SoftId = soft_id;

            // Открыли страницу от другого ПО - сохраняем параметры для перехода к списку
            if (_soft_id != soft_id)
            {
                _soft_id = soft_id;
                //_soft_filter = soft_filter;
                _soft_page = soft_page;
                _soft_sortExpression = soft_sortExpression;
            }

            ViewBag.SoftFilter = _soft_filter;
            ViewBag.SoftPage = _soft_page;
            ViewBag.SoftSortExpression = _soft_sortExpression;

            var inventoryContext = _context.RelSoftwareHardware
                .Include(r => r.RelshWhard)
                .Include(o => o.RelshWhard.WhardOffice)
                .Include(reg => reg.RelshWhard.WhardRegion)
                .Include(e => e.RelshWhard.RelHardwareEmployee.RelheEmployee)
                .Include(r => r.RelshWsoft)
                .Where(r => r.RelshWsoftId == soft_id)
                .AsQueryable();

            var _soft = _context.WealthSoftware.Find(soft_id);

            ViewBag.SoftName = _soft.WsoftName;

            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                inventoryContext = inventoryContext.Where(
                    e => EF.Functions.Like(e.RelshWhard.WhardName.ToUpper(), "%" + filter + "%")
                );
            }
            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                (
                   inventoryContext, pageSize, page, sortExpression, "RelshId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "soft_id", soft_id},
                { "filterInv", filterInv},                
                { "filterName", filterName},                
                { "filterRegion", filterRegion}
            };

            return View(model);
        }


        // GET: RelHardwareSoftwares/Create
        public IActionResult Create(
            int soft_id,
            string filter = "",
            int page = 1,
            string sortExpression = "RelshId",
            string filterInv = "",
            string filterName = "",
            string filterRegion = "",
            int soft_page = 1,
            string soft_sortExpression = "")
        {
            ViewBag.FilterInv = filterInv;
            ViewBag.FilterName = filterName;
            ViewBag.FilterRegion = filterRegion;
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.SoftId = soft_id;

            var _software = _context.WealthSoftware.Find(soft_id);
            var _hardware = _context.WealthHardware.Where(h => h.IsSoftDeployed == 1);

            ViewBag.SofrwareName = _software.WsoftName;

            ViewData["RelshWhardId"] = new SelectList(_hardware, "WhardId", "WhardName");

            return View();
        }


        // GET: Report
        public async Task<IActionResult> Report(
            string filter = "",
            string filterInv = "",
            string filterName = "",
            string filterRegion = "",
            int page = 1,
            string sortExpression = "WhardId")
        {
            ViewBag.FilterInv = filterInv;
            ViewBag.FilterName = filterName;
            ViewBag.FilterRegion = filterRegion;

            ViewBag.Filter = filter;
            ViewBag.Page = 1;
            ViewBag.SortExpression = "RelshId";

            ViewBag.SoftFilter = _soft_filter;
            ViewBag.SoftPage = _soft_page;
            ViewBag.SoftSortExpression = _soft_sortExpression;

            var inventoryContext = _context.RelSoftwareHardware
                .Include(r => r.RelshWhard)
                .Include(o => o.RelshWhard.WhardOffice)
                .Include(reg => reg.RelshWhard.WhardRegion)
                .Include(e => e.RelshWhard.RelHardwareEmployee.RelheEmployee)
                .Include(r => r.RelshWsoft)
                .Where(r => r.RelshWsoftId == _soft_id)
                .AsQueryable();

            int pageSize = 100000;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                inventoryContext = inventoryContext.Where(
                    e => EF.Functions.Like(e.RelshWhard.WhardName.ToUpper(), "%" + filter + "%")
                );
            }


            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                 (
                    inventoryContext, pageSize, 1, "RelshId", "RelshId"
                    );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "soft_id", _soft_id},
                { "filterInv", filterInv},
                { "filterName", filterName},
                { "filterRegion", filterRegion}
            };            

            return View(model);
        }
    }
}
