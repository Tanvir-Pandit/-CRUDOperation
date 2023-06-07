using System.ComponentModel.DataAnnotations;

namespace CRUDOperation.Models
{
    public class EmployeeAttendance
    {
        [Key]
        public int id { get; set; }
        public int employeeId { get; set; }
        public DateTime attendanceDate { get; set; }
        public bool isPresent { get; set; }
        public bool isAbsent { get; set; }
        public bool isOffday { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
