using EmployeeManagement.Models;

namespace EmployeeManagement.DTOs.Employee
{
    /// <summary>
    /// Data model to get employee details
    /// </summary>
    public class GetEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailId { get; set; }
        public JobTitle JobTitle { get; set; } = JobTitle.ProjectEngineer;
        public Mission Mission { get; set; } = Mission.SCV;
        public string ProjectName { get; set; }
        public string ReportsTo { get; set; }
    }
}