using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Shift_Scheduler.Models
{
    public class Employee
    {
        public Employee()
        {
            this.shifts = new HashSet<Shifts>();
        }

        [Key]
        public int employeeId { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }
        public string address { get; set; }
        public int phoneNumber { get; set; }
        public byte[] picture { get; set; }
        public string department { get; set; }

        public virtual ICollection<Shifts> shifts { get; set; }

    }
}