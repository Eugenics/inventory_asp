using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using inventory_dot_core.Models;
using Microsoft.EntityFrameworkCore;

namespace inventory_dot_core.Classes
{
    public class ControlesItems
    {
        private readonly InventoryContext _context;

        public ControlesItems(InventoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get positions by region for list controles
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetPositionsByRegion(int regionId)
        {
            if (regionId == 17 || regionId == 19) regionId = 24;
            if (regionId == 4) regionId = 22;

            var _positions = _context.Positions.Where(p => p.PositionDepartment.DepartmentRegionId == regionId)
                .Include(d => d.PositionDepartment);

            var retList = new List<SelectListItem>();

            foreach (var p in _positions)
            {
                if (p.PositionDepartment != null)
                    if (_context.Departments.Where(d => d.DepartmentId == p.PositionDepartmentId).AsNoTracking().Count() > 0)
                        retList.Add(new SelectListItem(p.PositionDepartment.DepartmentName + " | " + p.PositionName,
                            p.PositionId.ToString(), false, false));
            }
            return retList;
        }

        /// <summary>
        /// Get offices by region code for list controles
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetOfficesByRegion(int regionId)
        {
            if (regionId == 17 || regionId == 19) regionId = 24;
            if (regionId == 4) regionId = 22;

            var _offices = _context.Offices.Where(o => o.OfficeHouses.HousesRegionId == regionId)
                .Include(h => h.OfficeHouses);

            var retList = new List<SelectListItem>();

            foreach (var o in _offices)
            {
                if (o.OfficeHouses != null)
                    if (_context.Houses.Where(h => h.HousesId == o.OfficeHousesId).AsNoTracking().Count() > 0)
                        retList.Add(new SelectListItem(o.OfficeHouses.HousesName + " | " + o.OfficeName,
                            o.OfficeId.ToString(), false, false));
            }
            return retList;
        }
    }
}
