namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string? PhotoPath { get; set; }
        public int DeaprtmentId { get; set; }
        public Employee? Department { get; set; }
    }
}
