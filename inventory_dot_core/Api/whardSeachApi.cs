using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace inventory_dot_core.Api
{
    [Authorize(Policy = "RefEditorsRole")]
    [Route("api/[controller]")]
    public class whardSeachApi : Controller
    {
        private readonly InventoryContext _context;

        public whardSeachApi(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet("{regionId}/{officeId}/{searchString}")]
        public ActionResult GetJSONwhardSeach(int regionId, int officeId, string searchString)
        {
            var _hardwareInUse = _context.RelHardwareEmployee.Select(h => h.RelheWhardId).ToArray();

            // Получаем список не используемого оборудования
            var _hardwareNotInUse = _context.WealthHardware
                .Where(h => !_hardwareInUse.Contains(h.WhardId)
                && h.WhardRegionId == regionId
                && h.WhardOfficeId == officeId
                )
                .OrderBy(h => h.WhardInumber);

            if(searchString != "null")
            {
                _hardwareNotInUse = _hardwareNotInUse
                    .Where(h => h.WhardInumber.Contains(searchString)
                    || h.WhardName.Contains(searchString))
                    .OrderBy(h => h.WhardInumber);
            }

            var retList = new List<SelectListItem>();

            foreach (var h in _hardwareNotInUse)
            {
                retList.Add(new SelectListItem(h.WhardInumber + " | " + h.WhardName, h.WhardId.ToString(), false, false));
            }          

            return new JsonResult(retList);
        }

    }
}
