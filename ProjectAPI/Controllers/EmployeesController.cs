using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly LearningAngularDbContext _learningAngularDbContext;

        public EmployeesController(LearningAngularDbContext learningAngularDbContext)
        {
            _learningAngularDbContext = learningAngularDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees = await _learningAngularDbContext.Employees.ToListAsync();
           
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _learningAngularDbContext.Employees.AddAsync(employeeRequest);
            await _learningAngularDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
           var employee = await _learningAngularDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

           if (employee == null)
            {
                return NotFound();
            }

           return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _learningAngularDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _learningAngularDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _learningAngularDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            _learningAngularDbContext.Employees.Remove(employee);
            await _learningAngularDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
