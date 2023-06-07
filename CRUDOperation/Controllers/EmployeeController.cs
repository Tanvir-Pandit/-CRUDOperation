using CRUDOperation.Data;
using CRUDOperation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CRUDOperation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeAPIDbContext dbContext;
        private int employeeId;

        public EmployeeController(EmployeeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            return Ok(await dbContext.Employee.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            if (addEmployeeRequest == null)
            {
                return BadRequest("Invalid request data.");
            }

            var employee = new Employee()
            {
                employeeId = employeeId,
                employeeName = addEmployeeRequest.employeeName,
                employeeCode = addEmployeeRequest.employeeCode,
                employeeSalary = addEmployeeRequest.employeeSalary
            };

            if (dbContext != null)
            {
                await dbContext.Employee.AddAsync(employee);
                await dbContext.SaveChangesAsync();

                return Ok(employee);
            }

            return StatusCode(500, "Internal Server Error");
        }

        [HttpGet("GetEmployeeById/{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var employee = await dbContext.Employee.FindAsync(employeeId);

            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPut("UpdateEmployee/{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employee = await dbContext.Employee.FindAsync(employeeId);
            bool checkEmpCode = await dbContext.Employee.Where(f => f.employeeCode == updateEmployeeRequest.employeeCode).AnyAsync();
            if (employee != null && checkEmpCode == false && updateEmployeeRequest != null)
            {
                employee.employeeName = updateEmployeeRequest.employeeName;
                employee.employeeCode = updateEmployeeRequest.employeeCode;
                employee.employeeSalary = updateEmployeeRequest.employeeSalary;

                await dbContext.SaveChangesAsync();
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpDelete("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var employee = await dbContext.Employee.FindAsync(employeeId);

            if (employee != null)
            {
                dbContext.Remove(employee);
                await dbContext.SaveChangesAsync();
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpGet("GetEmployeeByMaxMinSalary/")]
        public async Task<IActionResult> GetEmployeeByMaxMinSalary()
        {
            var MaxSalary = await dbContext.Employee.MaxAsync( m=> m.employeeSalary);
            var MinSalary = await dbContext.Employee.MinAsync( m=> m.employeeSalary);

            var data = await dbContext.Employee.Where(f=> f.employeeSalary == MaxSalary || f.employeeSalary == MinSalary).ToListAsync();

            if (data != null)
            {
                return Ok(data);
            }

            return NotFound();
        }

        [HttpGet("GetAbsentEmployees")]
        public async Task<IActionResult> GetAbsentEmployees()
        {
            var absentEmployees = await dbContext.EmployeeAttendances.Include(i=>i.Employee)
                                                    .Where(e => e.isAbsent)
                                                    .Select(s=> new
                                                    {
                                                        Name = s.Employee.employeeName,
                                                        EmployeeId = s.Employee.employeeId,
                                                        EmployeeCode = s.Employee.employeeCode,
                                                        AttendanceDate = s.attendanceDate
                                                    })
                                                    .GroupBy(g=>g.EmployeeId).ToListAsync();

            if (absentEmployees != null)
            {
                return Ok(absentEmployees);
            }

            return NotFound();
        }

        [HttpGet("MonthlyAttendanceReport")]
        public async Task<IActionResult> MonthlyAttendanceReport()
        {
            var attendanceReport = await dbContext.EmployeeAttendances 
                .Include(a => a.Employee)
                .GroupBy(a => new { a.Employee.employeeId, a.Employee.employeeName, a.attendanceDate.Month })
                .Select(g => new
                {
                    EmployeeName = g.Key.employeeName,
                    MonthName = g.Key.Month,
                    TotalPresent = g.Count(a => a.isPresent),
                    TotalAbsent = g.Count(a => a.isAbsent),
                    TotalOffday = g.Count(a => a.isOffday)
                })
                .ToListAsync();

            if (attendanceReport != null)
            {
                return Ok(attendanceReport);
            }

            return NotFound();
        }


    }
}
