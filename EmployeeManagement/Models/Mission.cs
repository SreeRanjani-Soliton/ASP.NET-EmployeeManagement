using System.Text.Json.Serialization;

namespace EmployeeManagement.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Mission
    {
        SCV,
        D2T,
        GUI
    }
}