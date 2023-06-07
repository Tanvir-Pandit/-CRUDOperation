namespace CRUDOperation.Models
{
    public class UpdateEmployeeRequest
    {
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeCode { get; set; }
        public int employeeSalary { get; set; }
    }
}
