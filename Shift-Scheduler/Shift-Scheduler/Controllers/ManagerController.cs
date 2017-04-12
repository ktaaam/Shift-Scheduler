using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shift_Scheduler.Models
{
    // todo: only allow manager
    public class ManagerController : Controller
    {
        private ShiftContext db = new ShiftContext();
   

        enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

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
    
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostShifts(string startDate, ShiftEmp[] data)
        {
            var shifts = (from s in db.Shifts
                          select s.shiftId).ToList();

            foreach(ShiftEmp shift in data)
            {
                if (shift.empId == null || !shifts.Contains(shift.shiftId))                
                    return Json(new { error = "error" });                          
                else
                {
                    var day = (from s in db.Shifts
                                  where s.shiftId == shift.shiftId
                                  select new { s.dayOfTheWeek, s.shiftType }).FirstOrDefault();

                    Days dayval;
                    if (Enum.TryParse(day.dayOfTheWeek, out dayval))
                    {
                        DateTime start;
                        try
                        {
                            start = Convert.ToDateTime(startDate);
                        }
                        catch (FormatException)
                        {
                            return Json(new { error = "error" });
                        }

                        start.AddDays((int)dayval);

                        var exist = (from s in db.ShiftSchedules
                                     where s.date == start && s.shiftType == day.shiftType
                                     select s).FirstOrDefault();

                        int res;
                        if (!Int32.TryParse(shift.empId, out res))
                            return Json(new { error = "error" });

                        if (exist != null)
                        {
                            exist.empShiftScheduleID = res;
                            db.Entry(exist).State = System.Data.Entity.EntityState.Modified;

                        }
                        else
                        {
                            ShiftSchedule schedule = new ShiftSchedule();
                            schedule.date = start;
                            schedule.dayOfTheWeek = day.dayOfTheWeek;
                            schedule.empShiftScheduleID = res;
                            schedule.shiftType = day.shiftType;

                            db.ShiftSchedules.Add(schedule);
                        }

                        db.SaveChanges();                        
                    }
                    else
                    {
                        return Json(new { error = "error" });
                    }
                }
            }

            return Json(new {success = "success" });
        }

        public ActionResult employeeList()
        { 
            return View(db.Employees.ToList());
        }

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
         
            var res2 = (from s in db.Shifts
                        from e in s.employee
                        select new {s.shiftId,s.dayOfTheWeek,s.shiftType}).ToArray();

            for (int j = 0; j < res2.Length; j++)
            {
                string temp = res2[j].ToString();
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
            //foreach(var shiftChange in db.shiftChangeRequest)
            //{

            //}

            var kasjdfkljaslkdfjlakdf = db.shiftChangeRequest;

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

    public class ShiftEmp
    {
        public string empId { get; set; }
        public string shiftId { get; set; }
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