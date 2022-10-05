using EmployeeManagement.Models;

namespace EmployeeManagement.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
    }
}
