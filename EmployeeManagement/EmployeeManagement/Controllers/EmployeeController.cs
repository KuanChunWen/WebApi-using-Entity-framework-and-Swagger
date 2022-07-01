using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
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
        public IActionResult Get()
        {
            var employees = this.employeeDbContext.Employees.ToList();

            if (employees.Count == 0)
            {
                return StatusCode(404, "No employee found");
            }

            return Ok(employees);
        }

        [HttpPost("CreateEmployee")]
        public IActionResult Create([FromBody] CreateEmployeeViewModel input)
        {
            var employee = new Employee();
            employee.EmployeeId = input.EmployeeId;
            employee.Name = input.Name;
            employee.Dept = input.Dept;

            this.employeeDbContext.Employees.Add(employee);
            this.employeeDbContext.SaveChanges();

            var employees = this.employeeDbContext.Employees.ToList();

            return Ok(employees);
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult Update([FromBody] CreateEmployeeViewModel input)
        {
            var employee = this.employeeDbContext.Employees.FirstOrDefault(e => e.EmployeeId == input.EmployeeId);

            if (employee == null)
            {
                return StatusCode(404, "No employee found");
            }

            employee.EmployeeId = input.EmployeeId;
            employee.Name = input.Name;
            employee.Dept = input.Dept;

            this.employeeDbContext.Entry(employee).State = EntityState.Modified;
            this.employeeDbContext.SaveChanges();

            var employees = this.employeeDbContext.Employees.ToList();

            return Ok(employees);
        }

        [HttpDelete("DeleteEmployee/{employeeId}")]
        public IActionResult Delete([FromRoute]int employeeId)
        {
            var employee = this.employeeDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return StatusCode(404, "No employee found");
            }

            this.employeeDbContext.Entry(employee).State = EntityState.Deleted;
            this.employeeDbContext.SaveChanges();

            var employees = this.employeeDbContext.Employees.ToList();

            return Ok(employees);
        }
    }
}