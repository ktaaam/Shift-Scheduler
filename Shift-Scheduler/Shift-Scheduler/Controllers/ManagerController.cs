using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Shift_Scheduler.Models
{
    // todo: only allow manager
    public class ManagerController : Controller
    {
        private ShiftContext db = new ShiftContext();

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Shift()
        {
            ViewBag.shift = db.Shifts.ToList();

            return View();
        }

        public JsonResult GetShifts()
        {
            //List<Employee> output = new List<Employee>();

            List<KeyValuePair<string, List<ScheduleEmp>>> output = new List<KeyValuePair<string, List<ScheduleEmp>>>();          

            foreach (var shift in db.Shifts)
            {   
                var res = (from e in db.Employees
                          from s in e.shifts
                          where s.shiftId == shift.shiftId
                          select new { e.employeeId, e.firstName, e.lastName, e.phoneNumber }).ToList();

                List<ScheduleEmp> temp = new List<ScheduleEmp>();
                foreach(var result in res)
                {
                    temp.Add(new ScheduleEmp ( result.employeeId, result.firstName, result.lastName, result.phoneNumber ));
                }

                output.Add(new KeyValuePair<string, List<ScheduleEmp>>(shift.shiftId,temp));
            }

            //var json = JsonConvert.SerializeObject(output);           
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public ActionResult employeeList()
        { 
            return View(db.Employees.ToList());
        }

        public ActionResult dashBoard()
        {
            int dateNumber=  (int)DateTime.Today.DayOfWeek;
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string dayOfTheWeek = "";
            for(int i = 0; i < days.Length; i++)
            {
                if(dateNumber == i)
                {
                    dayOfTheWeek = days[i];
                }
            }
            var res = from e in db.Employees
                      from s in e.shifts
                      where s.dayOfTheWeek == "Monday"
                      select e;
           
            ViewBag.empAvail = res.ToList();
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }
    }

    public class ScheduleEmp
    {
        private int id;
        private string firstname;
        private string lastname;
        private string phone;

        public ScheduleEmp(int id, string firstname, string lastname, string phone)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.phone = phone;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }
}