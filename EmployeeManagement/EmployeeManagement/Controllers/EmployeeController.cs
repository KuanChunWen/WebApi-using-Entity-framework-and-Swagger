using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// 員工管理系統
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext employeeDbContext;

        public EmployeeController(
            EmployeeDBContext dbContext)
        {
            this.employeeDbContext = dbContext;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> Get()
        {
            var employees = await this.employeeDbContext.Employees.ToListAsync();

            if (employees.Count == 0)
            {
                return null;
            }

            return Ok(employees);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeViewModel input)
        {
            var employee = new Employee()
            {
                EmployeeId = input.EmployeeId,
                Name = input.Name,
                Dept = input.Dept,
            };

            await this.employeeDbContext.Employees.AddAsync(employee);
            await this.employeeDbContext.SaveChangesAsync();

            var employees = await this.employeeDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> Update([FromBody] CreateEmployeeViewModel input)
        {
            var employee = await this.employeeDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == input.EmployeeId);

            if (employee == null)
            {
                return null;
            }

            employee.EmployeeId = input.EmployeeId;
            employee.Name = input.Name;
            employee.Dept = input.Dept;

            this.employeeDbContext.Entry(employee).State= EntityState.Modified;
            await this.employeeDbContext.SaveChangesAsync();

            var employees = await this.employeeDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpDelete("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> Delete([FromRoute]int employeeId)
        {
            var employee = await this.employeeDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return null;
            }

            this.employeeDbContext.Entry(employee).State = EntityState.Deleted;
            await this.employeeDbContext.SaveChangesAsync();

            var employees = await this.employeeDbContext.Employees.ToListAsync();

            return Ok(employees);
        }
    }
}