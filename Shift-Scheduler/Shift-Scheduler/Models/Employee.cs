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

            ShiftSchedules = new List<ShiftSchedule>();
        }

        [Key]
        public int employeeId { get; set; }

        [StringLength(60, MinimumLength = 6)]
        [Required]
        public string userName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string passWord { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string firstName { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string lastName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string role { get; set; }
        public string address { get; set; }

        public string phoneNumber { get; set; }

        public byte[] picture { get; set; }
        public string department { get; set; }

        public virtual ICollection<Shifts> shifts { get; set; }

        public virtual ICollection<ShiftSchedule> ShiftSchedules { get; set; }
    }
}