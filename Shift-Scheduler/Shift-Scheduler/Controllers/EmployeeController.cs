using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shift_Scheduler.Models;

namespace Shift_Scheduler.Controllers
{
    public class EmployeeController : Controller
    {
        private Employee employee;
        private string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        private string[] shifts = { "Morning", "Evening", "Night" };

        // GET: Employee
        public ActionResult Index(int id = 0)
        {
            this.employee = new Employee() { employeeId = 1, firstName = "Tim", lastName = "Bufont", userName = "TBufont" };
            //SortedList<string, string> shifts = new SortedList<string, string>();

            //shifts.Add(new Shifts() { shiftId = "1", dayOfTheWeek = "Tuesday", shiftType = "Evening"});
            //shifts.Add(new Shifts() { shiftId = "2", dayOfTheWeek = "Wednesday", shiftType = "Morning" });
            //shifts.Add(new Shifts() { shiftId = "3", dayOfTheWeek = "Thursday", shiftType = "Evening" });
            //shifts.Add(new Shifts() { shiftId = "4", dayOfTheWeek = "Saturday", shiftType = "Night" });

            ViewData["Employee"] = employee;
            return View();
        }

        public ActionResult UserProfile()
        {
            return View("UserProfile");
        }

        public ActionResult ShiftChangeRequest()
        {

            return View();
        }

        public ActionResult VacationRequest()
        {
            return View();
        }

        public ActionResult ClockIn(int id)
        {
            return View();
        }

        public ActionResult ClockOut(int id)
        {
            return View();
        }
    }
}