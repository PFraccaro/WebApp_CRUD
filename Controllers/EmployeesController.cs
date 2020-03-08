using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_CRUD.Models;

namespace WebApp_CRUD.Controllers
{
    public class EmployeesController: Controller
    {
        private readonly NorthwindContext _context;
        
        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToArrayAsync());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            
            if (employee is null)
            {
                return RedirectToAction("Index");
            }
            
            return View(employee);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(Employees employee)
        {
            var employeeDb = await _context.Employees.FindAsync(employee.EmployeeId);
            
            if (employeeDb is null)
            {
                return RedirectToAction("Index");
            }

            employeeDb.FirstName = employee.FirstName;
            employeeDb.LastName = employee.LastName;

            await _context.SaveChangesAsync();
            
            return View(employee);
        }
    }
}