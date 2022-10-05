using EmployeeManagement.Contracts;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataAccess context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees.Include(em => em.Department).ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees.Include(em => em.Department).FirstOrDefaultAsync(em => em.Id == id) ?? new Employee();
        }
    }
}
