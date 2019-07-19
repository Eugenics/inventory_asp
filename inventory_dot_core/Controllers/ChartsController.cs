using System;
using System.Collections.Generic;
using System.Linq;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace inventory_dot_core.Controllers
{
    public class ChartsController : Controller
    {
        private readonly InventoryContext _context;

        public ChartsController(InventoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: <controller>
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var modelHardMol = _context.WealthHardware
                .Include(x => x.WhardRegion)
                .Include(x => x.WhardMolEmployee)
                .AsNoTracking()
                .GroupBy(x => x.WhardMolEmployee.EmployeeFullFio)
                .Select(grp => new
                {
                    MolEmp = grp.Key,
                    Total = grp.Count(),
                }).ToList();

            var modelSoft = _context.WealthSoftware
                .Include(x => x.WsoftRegion)
                .Include(x => x.WsoftWtype)
                .Include(x => x.WsoftWcat)
                .OrderBy(x => x.WsoftName)
                .AsNoTracking()
                .ToList();

            var modelHard = _context.WealthHardware
                .Include(x => x.WhardRegion)
                .OrderBy(x => x.WhardName)
                .AsNoTracking()
                .ToList();

            var grpSoftByRegion = modelSoft
                .GroupBy(o => o.WsoftRegion.RegionName)
                .Select(grp => new
                {
                    RegionName = grp.Key,
                    TotalSoft = grp.Sum(x => x.WsoftCnt),
                }).OrderBy(t => t.RegionName)
                .ToList();

            var grpHardByRegion = modelHard
                .GroupBy(x => x.WhardRegion.RegionName = x.WhardRegion.RegionName ?? "отсутствует")
                .Select(grp => new
                {
                    RegionName = grp.Key,
                    TotalHard = grp.Count(),
                }).OrderBy(t => t.RegionName)
                .ToList();

            List<object[]> lineCharts1 = new List<object[]>();
            object[] obj;

            foreach (var item in _context.Region)
            {
                int totalHard = grpHardByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault() != null
                    ? grpHardByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault().TotalHard : 0;

                int totalSoft = grpSoftByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault() != null
                   ? grpSoftByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault().TotalSoft : 0;

                obj= new object[3];

                obj[0] = item.RegionName;
                obj[1] = totalHard;
                obj[2] = totalSoft;

                lineCharts1.Add(obj);
            }

            var grpResultType = modelSoft
                .GroupBy(o => o.WsoftWtype.WtypeName)
                .Select(grp => new
                {
                    Name = grp.Key.ToString(),
                    Total = grp.Sum(x => x.WsoftCnt)
                }).OrderByDescending(t => t.Total)
                .ToList();

            ViewData["DataHardByMol"] = JsonConvert.SerializeObject(modelHardMol);
            ViewData["DataHardSoftByRegion"] = JsonConvert.SerializeObject(lineCharts1);
            ViewData["DataSoftByType"] = JsonConvert.SerializeObject(grpResultType);

            return View();
        }
    }
}
