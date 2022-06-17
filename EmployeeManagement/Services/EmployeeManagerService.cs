using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.DTOs.Employee;
using EmployeeManagement.Models;
using EmployeeManagement.Services.Database;

namespace EmployeeManagement.Services
{
    /// <summary>
    /// Class that implements controller's business logic
    /// </summary>
    public class EmployeeManagerService : IEmployeeManagerService
    {
        private List<Employee> _employees = new List<Employee>();
        private readonly IMapper _mapper;
        private IDataAccess _dataAccess;
        private string _tableName = "Employee";

        public EmployeeManagerService(IMapper mapper, IDataAccess dataAccess)
        {

            // _employees = new List<Employee> {
            //     new Employee() {Id=1,
            //                     Name = "Sree",
            //                     MailId = "sree@solitontech.com",
            //                     ProjectId = "ABC",
            //                     ReportsTo = "Teena"},
            //     new Employee() {Id=2,
            //                     Name = "Karthik",
            //                     MailId = "karthik@solitontech.com",
            //                     ProjectId = "CDE",
            //                     ReportsTo = "Arjun"}
            // };
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<ServiceResponse<List<GetEmployeeDTO>>> GetAllEmployees()
        {
            var resp = new ServiceResponse<List<GetEmployeeDTO>>();
            DataTable dt = _dataAccess.GetAllData("Employee");
            _employees = dataTableToEmps(dt);
            //Mapping to DTO
            resp.Data = _employees.Select(c => _mapper.Map<GetEmployeeDTO>(c)).ToList();
            return resp;
        }

        public async Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeById(int id)
        {
            var resp = new ServiceResponse<GetEmployeeDTO>();
            //var selEmp = _employees.FirstOrDefault(c => c.Id == id);
            DataRow dr = _dataAccess.GetSelectedData("Employee", "Id", id.ToString());
            if (dr != null)
            {
                var selEmp = dataRowToEmp(dr);
                //Converting to DTO
                resp.Data = _mapper.Map<GetEmployeeDTO>(selEmp);
            }
            else
            {
                resp.Data = null;
                resp.Status = false;
                resp.Message = "Employee of given ID not found";
            }
            return resp;
        }

        public async Task<ServiceResponse<List<GetEmployeeDTO>>> AddEmployee(AddEmployeeDTO newEmployee)
        {
            var resp = new ServiceResponse<List<GetEmployeeDTO>>();
            //Getting all property names and values 
            List<string> propNames = new();
            List<string> propValues = new();
            foreach (var prop in typeof(AddEmployeeDTO).GetProperties())
            {
                propNames.Add(prop.Name);
                propValues.Add(prop.GetValue(newEmployee, null).ToString());
            }
            //Invoking data access insert with property names and values
            DataTable dt = _dataAccess.InsertData(_tableName, propNames, propValues);
            if (dt != null)
            {
                _employees = dataTableToEmps(dt);
                //Converting to List of DTO
                resp.Data = _employees.Select(c => _mapper.Map<GetEmployeeDTO>(c)).ToList();
            }
            else
            {
                resp.Status = false;
                resp.Message = "Details couldn't be added";
            }

            return resp;
        }

        public async Task<ServiceResponse<GetEmployeeDTO>> UpdateEmployee(int id, UpdateEmployeeDTO updatedEmployee)
        {
            var resp = new ServiceResponse<GetEmployeeDTO>();
            //Getting all property names and values 
            List<string> propNames = new();
            List<string> propValues = new();
            foreach (var prop in typeof(UpdateEmployeeDTO).GetProperties())
            {
                propNames.Add(prop.Name);
                propValues.Add(prop.GetValue(updatedEmployee, null).ToString());
            }
            DataRow dr = _dataAccess.UpdateData(_tableName, "Id", id.ToString(), propNames, propValues);

            //Converting to DTO
            resp.Data = _mapper.Map<GetEmployeeDTO>(dataRowToEmp(dr));
            return resp;

        }

        public async Task<ServiceResponse<List<GetEmployeeDTO>>> DeleteEmployee(int id)
        {
            var resp = new ServiceResponse<List<GetEmployeeDTO>>();
            var selEmp = _employees.FirstOrDefault(c => c.Id == id);
            DataTable dt = _dataAccess.DeleteSelectedData(_tableName, "Id", id.ToString());
            if (dt != null)
            {
                _employees = dataTableToEmps(dt);
                resp.Data = _employees.Select(c => _mapper.Map<GetEmployeeDTO>(c)).ToList();
            }
            else
            {
                resp.Status = false;
                resp.Message = "Employee of given ID not found";
            }
            return resp;
        }

        //Helper functions
        private Employee dataRowToEmp(DataRow dr)
        {
            var employee = new Employee();
            employee.Id = (int)dr["Id"];
            employee.Name = dr["Name"].ToString().Trim();
            employee.MailId = dr["MailId"].ToString().Trim();
            employee.JobTitle = Enum.Parse<JobTitle>(dr["JobTitle"].ToString().Trim(), true);
            employee.Mission = Enum.Parse<Mission>(dr["Mission"].ToString().Trim(), true);
            employee.ProjectName = dr["ProjectName"].ToString().Trim();
            employee.ReportsTo = dr["ReportsTo"].ToString().Trim();
            return employee;
        }

        private List<Employee> dataTableToEmps(DataTable dt)
        {
            List<Employee> employees = new();
            foreach (DataRow dr in dt.Rows)
            {
                employees.Add(dataRowToEmp(dr));
            }
            return employees;
        }
    }
}