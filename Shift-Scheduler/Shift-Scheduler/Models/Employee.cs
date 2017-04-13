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

            shiftSchedules = new List<ShiftSchedule>();

            vacationRequests = new List<Vacation>();
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

<<<<<<< HEAD
        public virtual ICollection<ShiftSchedule> ShiftSchedules { get; set; }
        public virtual ICollection<ShiftChangeRequest> shiftChangeRequest { get; set; }
        public virtual ICollection<Vacation> vacation { get; set; }
=======
        public virtual ICollection<ShiftSchedule> shiftSchedules { get; set; }
        public virtual ICollection<Vacation> vacationRequests { get; set; }
>>>>>>> 62768169325efe7e8dd883f824e182d534c2a614
    }
}