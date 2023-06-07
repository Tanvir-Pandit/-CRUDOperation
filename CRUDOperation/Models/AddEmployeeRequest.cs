namespace CRUDOperation.Models
{
    public class AddEmployeeRequest
    {
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeCode { get; set; }
        public int employeeSalary { get; set; }
    }
}
