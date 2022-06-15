using System.Text.Json.Serialization;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// Mission
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Mission
    {
        SCV,
        D2T,
        GUI
    }
}