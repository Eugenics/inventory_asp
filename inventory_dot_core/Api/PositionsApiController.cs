using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using inventory_dot_core.Models;

namespace inventory_dot_core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsApiController : ControllerBase
    {
        private readonly InventoryContext _context;

        public PositionsApiController(InventoryContext context)
        {
            _context = context;
            //_ControleItems = new Classes.ControlesItems(new InventoryContext());
        }

        [HttpGet("{regionId}")]
        public ActionResult GetJSONPositions(int regionId)
        {
            return new JsonResult(new Classes.ControlesItems(_context).GetPositionsByRegion(regionId).OrderBy(x => x.Text));
        }
    }    
}