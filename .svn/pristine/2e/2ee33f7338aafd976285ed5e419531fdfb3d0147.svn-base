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
    public class SearchApiController : ControllerBase
    {
        private readonly InventoryContext _context;

        public SearchApiController(InventoryContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult HardwareSeach()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var hardwareList = _context.WealthHardware
                    .Where(h => h.WhardName.Contains(term) || h.WhardInumber.Contains(term))
                    .Select(h => h.WhardInumber + " | " + h.WhardName)
                    .ToList();

                return Ok(hardwareList);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}