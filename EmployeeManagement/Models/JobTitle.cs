using System.Text.Json.Serialization;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// Job description
    /// </summary>
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