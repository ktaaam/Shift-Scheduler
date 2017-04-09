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

        //public String[] getDateMissing()
        //{
        //    string[] dayType = { "MonMor", "MonEve", "MonNit", "TuesMor", "TuesEve", "TuesNit", "WedMor", "WedEve", "WedNit",
        //                         "ThurMor", "ThurEve", "ThurNit", "FriMor", "FriEve", "FriNit", "SatMor", "SatEve", "SatNit",
        //                         "SunMor", "SunEve", "SunNit", };

        //    var res3 = (from s in db.Shifts
        //                from e in s.employee
        //                select new { s.shiftId, s.dayOfTheWeek, s }).ToArray();

        //    for (int j = 0; j < res3.Length; j++)
        //    {
        //        string temp = res3[j].ToString();
        //        temp = temp.Trim(new Char[] { '{', '}' });
        //        String[] split = temp.Split(',');
        //        shift id value
        //       String[] split2 = split[0].Split(',');



        //    }
        //}

        public ActionResult dashBoard()
        {
            IQueryable<Shifts>[] output = new IQueryable<Shifts>[30];
            int dateNumber=  (int)DateTime.Today.DayOfWeek;
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string[] dayId = { "MonMor", "MonEve", "MonNit", "TuesMor", "TuesEve", "TuesNit", "WedMor", "WedEve", "WedNit",
                                 "ThurMor", "ThuEve", "ThuNit", "FriMor", "FriEve", "FriNit", "SatMor", "SatEve", "SatNit",
                                 "SunMor", "SunEve", "SunNit" };
            string[] dayType = { "Morning", "Evening", "Night" };
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
          
      
                var res3 = (from s in db.Shifts
                           from e in s.employee
                           select new {s.shiftId,s.dayOfTheWeek,s.shiftType}).ToArray();

            for (int j = 0; j < res3.Length; j++)
            {
                string temp = res3[j].ToString();
                temp = temp.Trim(new Char[] { '{', '}' });
                String[] split = temp.Split(',');
                // shift id value
                String[] shiftID = split[0].Split(',');
                // day of the weeek value
                String[] day = split[1].Split('=');
                // type of shift
                String[] type = split[2].Split('=');

                string dayWeek = day[1].Trim();
                string sType = type[1].Trim();
                
                string deleteId = "";

                for(int k = 0; k < days.Length; k++)
                {
                    int offset = 0;
                    if (dayWeek == days[k])
                    {
                        deleteId = days[k].Substring(0, 3);
                        days[k] = null;
                        List<string> tmp = days.OfType<string>().ToList();
                        tmp.RemoveAt(k);
                        days = tmp.ToArray();
                        for(int l = 0; l < dayType.Length; l++)
                        {
                            if(type[1] == dayType[l])
                            {
                                //offset for dayId
                                offset = l;
                                //delete the element in the array
                                deleteId = dayType[k].Substring(0, 3);
                                List<string> typeShift = dayType.OfType<string>().ToList();
                                typeShift.RemoveAt(l);
                                dayType = typeShift.ToArray();
                            }
                        }
                        
                        
                        for(; offset < dayId.Length;)
                        {
                            if(deleteId == dayId[offset])
                            {
                                List<string> tempId = dayId.OfType<string>().ToList();
                                tempId.RemoveAt(offset);
                                dayId = tempId.ToArray();
                            }
                            offset += 3;
                        }
                    }
                }
            }
            ViewBag.shiftDay = days.ToList();
            ViewBag.shiftType = dayType.ToList();
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