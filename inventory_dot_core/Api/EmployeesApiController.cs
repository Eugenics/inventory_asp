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
    public class EmployeesApiController : ControllerBase
    {
        private readonly InventoryContext _context;

        public EmployeesApiController(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet("{regionId}")]
        public ActionResult GetJSONEmployees(int regionId)
        {
            return new JsonResult(new Classes.ControlesItems(_context).GetEmployeesByRegion(regionId).OrderBy(x => x.Text));
        }
    }
}