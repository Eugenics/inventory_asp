using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using inventory_dot_core.Models;

namespace inventory_dot_core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesApiController : ControllerBase
    {
        private readonly InventoryContext _context;

        public HousesApiController(InventoryContext context)
        {
            _context = context;
            //_ControleItems = new Classes.ControlesItems(new InventoryContext());
        }

        [HttpGet("{regionId}")]
        public ActionResult GetJSONOffices(int regionId)
        {
            return new JsonResult(new Classes.ControlesItems(_context).GetHousesByRegion(regionId).OrderBy(x => x.Text));
        }
    }
}