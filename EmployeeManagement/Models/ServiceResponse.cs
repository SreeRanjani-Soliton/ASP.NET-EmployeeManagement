namespace EmployeeManagement.Models
{
    /// <summary>
    /// Service response to be given to client
    /// </summary>
    /// <typeparam name="T">Type of data in response</typeparam>
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; } = true;
        public string Message { get; set; } = null;
    }
}