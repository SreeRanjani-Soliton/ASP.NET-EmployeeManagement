using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.DTOs.Employee;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    /// <summary>
    /// Interface that defines controller's business logic
    /// </summary>
    public interface IEmployeeManagerService
    {
        Task<ServiceResponse<List<GetEmployeeDTO>>> GetAllEmployees();
        Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeById(int id);
        Task<ServiceResponse<List<GetEmployeeDTO>>> AddEmployee(AddEmployeeDTO newEmployee);
        Task<ServiceResponse<GetEmployeeDTO>> UpdateEmployee(int id, UpdateEmployeeDTO updateEmployee);
        Task<ServiceResponse<List<GetEmployeeDTO>>> DeleteEmployee(int id);
    }
}