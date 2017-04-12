using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.Models
{
    public class ShiftChangeRequest
    {
        [Key]
        public int shiftChangeRequestId { get; set; }
        public string shiftApproval { get; set; }
        public int shiftScheduleID { get; set; }

        [ForeignKey("shiftScheduleID")]
        public virtual ShiftSchedule shiftSchedule { get; set; }
        public virtual Employee currentWorkingEmp { get; set; }
        public virtual Employee newWorkingEmp{ get; set; }
    }
}