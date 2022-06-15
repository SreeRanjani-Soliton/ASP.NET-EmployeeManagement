using System.Text.Json.Serialization;

namespace EmployeeManagement.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JobTitle
    {
        ProjectEngineer,
        SeniorProjectEngineer,
        ProjectLead,
        SeniorProjectLead,
        ProjectManager,
        SeniorProjectManager
    }
}