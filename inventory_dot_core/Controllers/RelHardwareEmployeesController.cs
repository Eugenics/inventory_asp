using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_dot_core.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace inventory_dot_core.Controllers
{
    [Authorize(Policy = "RefEditorsRole")]
    public class RelHardwareEmployeesController : Controller
    {
        private readonly InventoryContext _context;

        private static string _emp_filter;
        private static int _emp_page;
        private static string _emp_sortExpression;
        //private static int indexPageOpnCnt = 0;
        private static int _employee_id;

        public RelHardwareEmployeesController(InventoryContext context)
        {
            _context = context;
        }

        // GET: RelHardwareEmployees
        public async Task<IActionResult> Index(int employee_id, string filter = "", int page = 1, string sortExpression = "RelheId",
            string emp_filter = "", int emp_page = 1, string emp_sortExpression = "EmployeeId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.EmployeeId = employee_id;

            // Открыли страницу от другого сотрудника - сохраняем параметры для перехода к сотрудникам
            if (_employee_id != employee_id)
            {
                _employee_id = employee_id;
                _emp_filter = emp_filter;
                _emp_page = emp_page;
                _emp_sortExpression = emp_sortExpression;
            }

            ViewBag.EmpFilter = _emp_filter;
            ViewBag.EmpPage = _emp_page;
            ViewBag.EmpSortExpression = _emp_sortExpression;

            var inventoryContext = _context.RelHardwareEmployee
                .Include(r => r.RelheEmployee)
                .Include(r => r.RelheWhard)
                .Where(e => e.RelheEmployeeId == employee_id)
                .AsQueryable();

            var _employee = _context.Employees.Find(employee_id);

            ViewBag.EmployeeName = _employee.EmployeeFullFio;

            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToUpper();
                inventoryContext = inventoryContext.Where(
                    e => EF.Functions.Like(e.RelheWhard.WhardName.ToUpper(), "%" + filter + "%")
                );
            }
            var model = await inventory_dot_core.Classes.Paging.PagingList.CreateAsync
                (
                   inventoryContext, pageSize, page, sortExpression, "RelheId"
                   );

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: RelHardwareEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relHardwareEmployee = await _context.RelHardwareEmployee
                .Include(r => r.RelheEmployee)
                .Include(r => r.RelheWhard)
                .FirstOrDefaultAsync(m => m.RelheId == id);
            if (relHardwareEmployee == null)
            {
                return NotFound();
            }

            return View(relHardwareEmployee);
        }

        // GET: RelHardwareEmployees/Create
        public IActionResult Create(int employee_id, string filter = "", int page = 1, string sortExpression = "RelheId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.EmployeeId = employee_id;

            var _employee = _context.Employees.Find(employee_id);

            ViewBag.EmployeeName = _employee.EmployeeFullFio;
            

            ViewData["RelheEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname")
                .Where(e => e.Value == employee_id.ToString());
            ViewData["RelheWhardId"] = new SelectList(this.GetNotUseHardList(0,_employee.EmployeeRegionId), "WhardId", "WhardName");
            return View();
        }

        // POST: RelHardwareEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelheId,RelheEmployeeId,RelheWhardId")] RelHardwareEmployee relHardwareEmployee,
            int employee_id, string filter = "", int page = 1, string sortExpression = "RelheId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
                        

            var _employee = _context.Employees.Find(employee_id);

            ViewBag.EmployeeName = _employee.EmployeeFullFio;
            //ViewBag.EmployeeId = _employee.EmployeeId;

            relHardwareEmployee.RelheEmployeeId = employee_id;

            ModelState.Clear();


            if (ModelState.IsValid)
            {
                _context.Add(relHardwareEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),
                    new
                    {
                        employee_id = employee_id,
                        filter = filter,
                        page = page,
                        sortExpression = sortExpression
                    });
            }

           
            ViewData["RelheEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFullFio")
                .Where(e => e.Value == employee_id.ToString());
            ViewData["RelheWhardId"] = new SelectList(this.GetNotUseHardList(0,relHardwareEmployee.RelheEmployee.EmployeeRegionId), "WhardId", "WhardName");

            return View(relHardwareEmployee);
        }

        // GET: RelHardwareEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id, int employee_id, string filter = "", int page = 1, string sortExpression = "RelheId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.EmployeeId = employee_id;

            if (id == null)
            {
                return NotFound();
            }

            var relHardwareEmployee = await _context.RelHardwareEmployee.FindAsync(id);
            if (relHardwareEmployee == null)
            {
                return NotFound();
            }
            ViewData["RelheEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname", relHardwareEmployee.RelheEmployeeId);
            ViewData["RelheWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", relHardwareEmployee.RelheWhardId);
            return View(relHardwareEmployee);
        }

        // POST: RelHardwareEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RelheId,RelheEmployeeId,RelheWhardId")] RelHardwareEmployee relHardwareEmployee,
            int employee_id, string filter = "", int page = 1, string sortExpression = "RelheId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.EmployeeId = employee_id;

            if (id != relHardwareEmployee.RelheId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relHardwareEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelHardwareEmployeeExists(relHardwareEmployee.RelheId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RelheEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname", relHardwareEmployee.RelheEmployeeId);
            ViewData["RelheWhardId"] = new SelectList(_context.WealthHardware, "WhardId", "WhardId", relHardwareEmployee.RelheWhardId);
            return View(relHardwareEmployee);
        }

        // GET: RelHardwareEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id, int employee_id, string filter = "", int page = 1, string sortExpression = "RelheId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.EmployeeId = employee_id;            

            if (id == null)
            {
                return NotFound();
            }

            var relHardwareEmployee = await _context.RelHardwareEmployee
                .Include(r => r.RelheEmployee)
                .Include(r => r.RelheWhard)
                .FirstOrDefaultAsync(m => m.RelheId == id);
            if (relHardwareEmployee == null)
            {
                return NotFound();
            }

            return View(relHardwareEmployee);
        }

        // POST: RelHardwareEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int employee_id, string filter = "", int page = 1, string sortExpression = "RelheId")
        {
            ViewBag.Filter = filter;
            ViewBag.Page = page;
            ViewBag.SortExpression = sortExpression;
            ViewBag.EmployeeId = employee_id;

            var relHardwareEmployee = await _context.RelHardwareEmployee.FindAsync(id);
            _context.RelHardwareEmployee.Remove(relHardwareEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),
                new
                {
                    employee_id = employee_id,
                    filter = filter,
                    page = page,
                    sortExpression = sortExpression
                });
        }

        private bool RelHardwareEmployeeExists(int id)
        {
            return _context.RelHardwareEmployee.Any(e => e.RelheId == id);
        }

        private IQueryable GetNotUseHardList(int curWhardId = 0, int regionId = 0)
        {
            // Создаем список используемого оборудования
            var _hardwareInUse = _context.RelHardwareEmployee.Select(h => h.RelheWhardId).ToArray();

            // Удаляем из списка используемого оборудования текущее. Это необходимо для списка при редактировании.
            if (curWhardId != 0)
            {
                _hardwareInUse = _hardwareInUse.Where(l => l != curWhardId).ToArray();
            }

            // Получаем список не используемого оборудования
            var _hardwareNotInUse = _context.WealthHardware
                .Where(h => !_hardwareInUse.Contains(h.WhardId)
                && h.WhardRegionId == regionId);

            return _hardwareNotInUse;
        }

        [Route("Contact")]
        public async Task<IActionResult> Contact()
        {
            //var model = _context.WealthHardware.AsQueryable();

            return PartialView("_hardWareModalFrm", await _context.WealthHardware.ToListAsync());
        }
    }
}
