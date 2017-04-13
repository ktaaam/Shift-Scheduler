using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.ViewModel
{
    public class DashBoardViewModel
    {
        //Employee
        public int employeeId { get; set; }
        public string employeeFirstName { get; set; }
        public string employeeLastName { get; set; }
        public string role { get; set; }
        public string phoneNumber { get; set; }

        //Shift Change Request
        public int shiftChangeRequestId { get; set; }
        public string shiftApproval { get; set; }
        public int shiftScheduleID { get; set; }
        public  string currentWorkingEmpFirstName{ get; set; }
        public string currentWorkingEmpLastName { get; set; }
        public int currentWorkingEmpId { get; set; }
        public  string newWorkingEmpFirstName { get; set; }
        public string newWorkingEmpLastName { get; set; }
        public int newWorkingEmpId { get; set; }



        //vacation

        public int vacationID { get; set; }

        public DateTime dateStart { get; set; }

        public DateTime dateEnd { get; set; }

        public string approvalStatus { get; set; }

        public int empVacationRequestID { get; set; }


    }
}