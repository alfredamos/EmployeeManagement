using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class DataAccess : DbContext
    {
        public DataAccess(DbContextOptions<DataAccess> options) : base(options) { }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Employee> Employees => Set<Employee>();
    }
}
