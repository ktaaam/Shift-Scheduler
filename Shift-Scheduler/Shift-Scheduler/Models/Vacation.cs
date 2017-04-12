using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.Models
{
    public class Vacation
    {
        public int vacationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public virtual Employee employee { get; set; }
    }
}