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

            retList.Add(new SelectListItem("Выберите значение", "0", false, false));

            foreach (var o in _offices)
            {
                if (o.OfficeHouses != null)
                    if (_context.Houses.Where(h => h.HousesId == o.OfficeHousesId).AsNoTracking().Count() > 0)
                        retList.Add(new SelectListItem(o.OfficeHouses.HousesName + " | " + o.OfficeName,
                            o.OfficeId.ToString(), false, false));
            }
            return retList;
        }

        /// <summary>
        /// Get offices by house code for list controles
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetOfficesByHouse(int houseId)
        {
            var _offices = _context.Offices.Where(o => o.OfficeHousesId == houseId)
                .Include(h => h.OfficeHouses);

            var retList = new List<SelectListItem>();

            retList.Add(new SelectListItem("Выберите значение", "0", false, false));

            foreach (var o in _offices)
            {
                if (o.OfficeHouses != null)
                    if (_context.Houses.Where(h => h.HousesId == o.OfficeHousesId).AsNoTracking().Count() > 0)
                        retList.Add(new SelectListItem(o.OfficeHouses.HousesName + " | " + o.OfficeName,
                            o.OfficeId.ToString(), false, false));
            }
            return retList;
        }

        /// <summary>
        /// Get houses by region code for list controles
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetHousesByRegion(int regionId)
        {
            if (regionId == 17 || regionId == 19) regionId = 24;
            if (regionId == 4) regionId = 22;

            var _offices = _context.Houses.Where(o => o.HousesRegionId == regionId)
                .Include(h => h.HousesRegion);

            var retList = new List<SelectListItem>();

            retList.Add(new SelectListItem("Выберите значение", "0", false, false));

            foreach (var o in _offices)
            {
                if (o.HousesRegion != null)
                    retList.Add(new SelectListItem(o.HousesRegion.RegionName + " | " + o.HousesName,
                            o.HousesId.ToString(), false, false));
            }
            return retList;
        }

        /// <summary>
        /// Get departments by region code for list controles
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetDepartmentsByRegion(int regionId)
        {
            if(regionId == 17 || regionId == 19) regionId = 24;
            if (regionId == 4) regionId = 22;

            var _departments = _context.Departments.Where(d => d.DepartmentRegionId == regionId)
                .Include(r => r.DepartmentRegion);
            var retList = new List<SelectListItem>();

            foreach(var d in _departments)
            {
                if (d.DepartmentRegion != null)
                    retList.Add(new SelectListItem(d.DepartmentRegion.RegionName + " | " + d.DepartmentName,
                        d.DepartmentId.ToString(), false, false));
            }

            return retList;


        }


        /// <summary>
        /// Get employees by region code for list controles
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetEmployeesByRegion(int regionId)
        {
            if (regionId == 17 || regionId == 19) regionId = 24;
            if (regionId == 4) regionId = 22;

            var _employees = _context.Employees.Where(e => e.EmployeeRegionId == regionId)
                .Include(d => d.EmployeePosition.PositionDepartment);
            var retList = new List<SelectListItem>();

            foreach (var e in _employees)
            {
                if (e.EmployeePosition.PositionDepartment != null)
                    retList.Add(new SelectListItem(e.EmployeePosition.PositionDepartment.DepartmentName + " | " + e.EmployeeFullFio,
                        e.EmployeeId.ToString(), false, false));
            }

            return retList;
        }


        /// <summary>
        /// Get mol employees by region code for list controles
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetMOLEmployeesByRegion(int regionId)
        {
            if (regionId == 17 || regionId == 19) regionId = 24;
            if (regionId == 4) regionId = 22;

            var _employees = _context.Employees.Where(e => e.EmployeeRegionId == regionId && e.EmployeeIsMol == 1)
                .Include(d => d.EmployeePosition.PositionDepartment);
            var retList = new List<SelectListItem>();

            foreach (var e in _employees)
            {
                if (e.EmployeePosition.PositionDepartment != null)
                    retList.Add(new SelectListItem(e.EmployeePosition.PositionDepartment.DepartmentName + " | " + e.EmployeeFullFio,
                        e.EmployeeId.ToString(), false, false));
            }

            return retList;
        }
    }
}
