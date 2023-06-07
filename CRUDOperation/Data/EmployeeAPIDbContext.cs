using CRUDOperation.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperation.Data
{
    public class EmployeeAPIDbContext : DbContext
    {
        public EmployeeAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
    }
}
