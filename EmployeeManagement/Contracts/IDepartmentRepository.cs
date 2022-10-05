using EmployeeManagement.Models;

namespace EmployeeManagement.Contracts
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
    }
}
