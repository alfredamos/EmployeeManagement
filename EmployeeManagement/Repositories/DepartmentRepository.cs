using EmployeeManagement.Contracts;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataAccess context) : base(context)
        {
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.Include(dp => dp.Employees).ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _context.Departments.Include(dp => dp.Employees).FirstOrDefaultAsync(dp => dp.Id == id) ?? new Department();
        }
    }

}
