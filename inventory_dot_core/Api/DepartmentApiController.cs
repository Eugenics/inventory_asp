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
    public class DepartmentApiController : ControllerBase
    {
        private readonly InventoryContext _context;

        public DepartmentApiController(InventoryContext context)
        {
            _context = context;
            //_ControleItems = new Classes.ControlesItems(new InventoryContext());
        }

        [HttpGet("{regionId}")]
        public ActionResult GetJSONDepartments(int regionId)
        {
            return new JsonResult(new Classes.ControlesItems(_context).GetDepartmentsByRegion(regionId).OrderBy(x => x.Text));
        }

        [HttpGet]
        public ActionResult GetJSONDepartmentFull()
        {
            object listDep = new Classes.ControlesItems(_context).GetDepartments();
            return new JsonResult(listDep);
        }
    }
}