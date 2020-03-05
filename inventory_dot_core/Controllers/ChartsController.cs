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
using Microsoft.AspNetCore.Authorization;

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
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
                .Include(t => t.WhardWtype)
                .AsNoTracking()
                .ToList();

            var modelRelHE = _context.RelHardwareEmployee
                .Include(w => w.RelheWhard)
                .Include(r => r.RelheWhard.WhardRegion)
                .Include(t => t.RelheWhard.WhardWtype)
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

            var HardInUse = modelRelHE
                    .Where(t => t.RelheWhard.WhardWtype.wtype_is_it == 1)
                    .GroupBy(h => h.RelheWhard.WhardRegion.RegionName)
                    .Select(hgroup => new
                    {
                        RegionName = hgroup.Key,
                        TotalHardInUse = hgroup.Count()
                    })
                    .OrderBy(h => h.RegionName)
                    .ToList();
            
            var TotalITHard = modelHard
                .Where(t => t.WhardWtype.wtype_is_it == 1)
                .GroupBy(h => h.WhardRegion.RegionName)
                .Select(hgroup => new
                {
                    RegionName = hgroup.Key,
                    TotalITHardCnt= hgroup.Count()
                })
                    .OrderBy(h => h.RegionName)
                    .ToList();



            List<object> HardChart = new List<object>();
            object obj;

            foreach (var item in _context.Region)
            {
                int totalHard = grpHardByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault() != null
                    ? grpHardByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault().TotalHard : 0;

                int totalSoft = grpSoftByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault() != null
                   ? grpSoftByRegion.Where(x => x.RegionName == item.RegionName).FirstOrDefault().TotalSoft : 0;

                int hardInUse = HardInUse.Where(x => x.RegionName == item.RegionName).FirstOrDefault() != null
                    ? HardInUse.Where(x => x.RegionName == item.RegionName).FirstOrDefault().TotalHardInUse : 0;

                int TotalITHardCnt = TotalITHard.Where(x => x.RegionName == item.RegionName).FirstOrDefault() != null
                    ? TotalITHard.Where(x => x.RegionName == item.RegionName).FirstOrDefault().TotalITHardCnt : 0;


                obj = new
                {
                    RegionName = item.RegionName,
                    totalHard = totalHard,
                    totalSoft = totalSoft,
                    TotalHardInUse = hardInUse,
                    TotalITHardCnt = TotalITHardCnt
                };

                HardChart.Add(obj);
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
            ViewData["DataHardSoftByRegion"] = JsonConvert.SerializeObject(HardChart);
            ViewData["DataSoftByType"] = JsonConvert.SerializeObject(grpResultType);            

            return View();
        }
    }
}
