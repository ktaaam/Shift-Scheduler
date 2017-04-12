using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> 62768169325efe7e8dd883f824e182d534c2a614
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.Models
{
    public class Vacation
    {
<<<<<<< HEAD
        public int vacationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public virtual Employee employee { get; set; }
=======
        [Key]
        public int vacationID { get; set; }

        public DateTime dateStart { get; set; }

        public DateTime dateEnd { get; set; }

        public Boolean approvalStatus { get; set; }

        public int empVacationRequestID { get; set; }

        [ForeignKey("empVacationRequestID")]
        public virtual Employee Employees { get; set; }




>>>>>>> 62768169325efe7e8dd883f824e182d534c2a614
    }
}