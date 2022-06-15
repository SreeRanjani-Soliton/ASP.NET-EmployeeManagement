namespace EmployeeManagement.Models
{
    /// <summary>
    /// Core employee class
    /// </summary>
    public class Employee
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