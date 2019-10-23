using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Authorization;

namespace inventory_dot_core.Api
{
    [Authorize(Policy = "RefEditorsRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesApiController : ControllerBase
    {
        private readonly InventoryContext _context;

        public OfficesApiController(InventoryContext context)
        {
            _context = context;
            //_ControleItems = new Classes.ControlesItems(new InventoryContext());
        }

        [HttpGet("{regionId}")]
        public ActionResult GetJSONOfficesByRegion(int regionId)
        {
            return new JsonResult(new Classes.ControlesItems(_context).GetOfficesByRegion(regionId));
        }

        [HttpGet]
        [Route("[action]/{houseId}")]
        public ActionResult GetJSONOfficesByHouse(int houseId)
        {
            return new JsonResult(new Classes.ControlesItems(_context).GetOfficesByHouse(houseId));
        }
    }
}