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
    public class MolApiController : ControllerBase
    {
        private readonly InventoryContext _context;

        public MolApiController(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet("{regionId}")]
        public ActionResult GetJSONMol(int regionId)
        {
            return new JsonResult(new Classes.ControlesItems(_context).GetMOLEmployeesByRegion(regionId).OrderBy(x => x.Text));
        }
    }
}