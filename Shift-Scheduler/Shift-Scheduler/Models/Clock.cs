using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.Models
{
    public class Clock
    {
        [Key]
        public int clockId { get; set; }
        public DateTime clockIn { get; set; }
        public Nullable<DateTime> clockOut { get; set; }
        public int empClockID { get; set; }
        [ForeignKey("empClockID")]
        public virtual Employee Employees { get; set; }
    }
}