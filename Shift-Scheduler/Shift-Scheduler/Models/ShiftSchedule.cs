using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.Models
{
    public class ShiftSchedule
    {
        
        [Key]
        public int shiftScheduleId { get; set; }
        public DateTime date { get; set; }
        public string dayOfTheWeek { get; set; }
        public string shiftType { get; set; }
        public int empShiftScheduleID { get; set; }
        [ForeignKey("empShiftScheduleID")]
        public virtual Employee Employees { get; set; }



    }



}