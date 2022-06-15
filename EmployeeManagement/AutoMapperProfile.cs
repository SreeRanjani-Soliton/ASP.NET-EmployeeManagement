using AutoMapper;
using EmployeeManagement.DTOs.Employee;
using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, GetEmployeeDTO>();
            CreateMap<AddEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();
        }
    }
}