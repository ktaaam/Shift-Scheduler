using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.ViewModel
{
    public class VacationViewModel
    {


            public string employeeFirstName { get; set; }
            public string employeeLastName { get; set; }

            //vacation

            public int vacationID { get; set; }

            public DateTime dateStart { get; set; }

            public DateTime dateEnd { get; set; }

            public string approvalStatus { get; set; }

            public int empVacationRequestID { get; set; }

        }

}