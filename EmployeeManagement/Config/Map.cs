using AutoMapper;
using EmployeeManagement.Models;

namespace EmployeeManagement.Config
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<Department, Department>();
            CreateMap<Employee, Employee>();
        }
    }
}
