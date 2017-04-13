using System.Collections.Generic;
using System.ComponentModel;
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

            clock = new List<Clock>();
        }

        [Key]
        public int employeeId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string address { get; set; }

        public string phoneNumber { get; set; }

        public byte[] picture { get; set; }
        public string department { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        [DefaultValue(false)]
        public bool RememberMe { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Shifts> shifts { get; set; }
        public virtual ICollection<ShiftChangeRequest> shiftChangeRequest { get; set; }
        public virtual ICollection<ShiftSchedule> shiftSchedules { get; set; }
        public virtual ICollection<Vacation> vacationRequests { get; set; }
        public virtual ICollection<Clock> clock { get; set; }

    }
}