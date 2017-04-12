using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;


namespace Shift_Scheduler.Models
{
    public class Shifts
    {
        public Shifts()
        {
            this.employee = new HashSet<Employee>();

        }

        [Key]
        public string shiftId { get; set; }
        public string dayOfTheWeek { get; set; }
        public string shiftType { get; set; }
        public virtual ICollection<Employee> employee { get; set; }

    } 
}