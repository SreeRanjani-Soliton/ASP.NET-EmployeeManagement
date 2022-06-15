using EmployeeManagement.Models;

namespace EmployeeManagement.DTOs.Employee
{
    public class AddEmployeeDTO
    {
        public string Name { get; set; }
        public string MailId { get; set; }
        public JobTitle JobTitle { get; set; } = JobTitle.ProjectEngineer;
        public Mission Mission { get; set; } = Mission.SCV;
        public string ProjectName { get; set; }
        public string ReportsTo { get; set; }
    }
}