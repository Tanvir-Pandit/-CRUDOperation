namespace CRUDOperation.Models
{
    public class Employee
    {
        internal bool isAbsent;

        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeCode { get; set; }
        public int employeeSalary { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set;}
    }
}
