using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shift_Scheduler.Models
{
    // todo: only allow manager
    public class ManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


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
                foreach (var result in res)
                {
                    temp.Add(new ScheduleEmp(result.employeeId, result.firstName, result.lastName, result.phoneNumber));
                }

                output.Add(new KeyValuePair<string, List<ScheduleEmp>>(shift.shiftId, temp));
            }

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostShifts(string startDate, ShiftEmp[] data)
        {
            var shifts = (from s in db.Shifts
                          select s.shiftId).ToList();

            //return Json(data);

            foreach (ShiftEmp shift in data)
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

                        start = start.AddDays((int)dayval);

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

            return Json(new { success = "success" });
        }

        public ActionResult employeeList()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult dashBoard()
        {
            IQueryable<Shifts>[] output = new IQueryable<Shifts>[30];
            int dateNumber = (int)DateTime.Today.DayOfWeek;
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string[] dayId = { "MonMor", "MonEve", "MonNit", "TuesMor", "TuesEve", "TuesNit", "WedMor", "WedEve", "WedNit",
                                 "ThurMor", "ThuEve", "ThuNit", "FriMor", "FriEve", "FriNit", "SatMor", "SatEve", "SatNit",
                                 "SunMor", "SunEve", "SunNit" };
            string[] dayType = { "Morning", "Evening", "Night" };
            string dayOfTheWeek = "";
            for (int i = 0; i < days.Length; i++)
            {
                if (dateNumber == i)
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
                        select new { s.shiftId, s.dayOfTheWeek, s.shiftType }).ToArray();

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

                for (int k = 0; k < days.Length; k++)
                {
                    int offset = 0;
                    if (dayWeek == days[k])
                    {
                        deleteId = days[k].Substring(0, 3);
                        //days[k] = null;
                        List<string> tmp = days.OfType<string>().ToList();
                        tmp.RemoveAt(k);
                        days = tmp.ToArray();
                        for (int l = 0; l < dayType.Length; l++)
                        {
                            if (type[1] == dayType[l])
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


                        for (; offset < dayId.Length;)
                        {
                            if (deleteId == dayId[offset])
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
            ViewBag.shift = db.Shifts.ToList();

            return View();
        }

        public JsonResult GetShiftSchedule()
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var result = (from s in db.ShiftSchedules
                          from e in db.Employees
                          where s.date >= startOfWeek && s.date <= endOfWeek && s.empShiftScheduleID == e.employeeId
                          select new { s.dayOfTheWeek, s.shiftType, e.firstName, e.lastName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeShift()
        {
            return View();
        }

        public JsonResult GetShiftChange()
        {
            var res = (from c in db.shiftChangeRequest
                       from s in db.ShiftSchedules
                       from e in db.Employees
                       where c.shiftScheduleID == s.shiftScheduleId && c.newWorkingEmp.employeeId == e.employeeId && c.shiftApproval == "pending"
                       select new shiftChangeEmp { id = c.shiftChangeRequestId, date = s.date, shiftType = s.shiftType, curFirstName = c.currentWorkingEmp.firstName, curLastName = c.currentWorkingEmp.lastName, newFirstName = e.firstName, newLastName = e.lastName }).ToList();

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RequestApprove(int id)
        {
            var res = (from c in db.shiftChangeRequest
                       where c.shiftChangeRequestId == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.shiftApproval = "approved";
                var result = (from s in db.ShiftSchedules
                              where res.shiftScheduleID == s.shiftScheduleId
                              select s).FirstOrDefault();

                result.empShiftScheduleID = res.newWorkingEmp.employeeId;

                db.Entry(result).State = EntityState.Modified;
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ChangeShift");
        }

        public ActionResult RequestDeny(int id)
        {
            var res = (from c in db.shiftChangeRequest
                       where c.shiftChangeRequestId == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.shiftApproval = "denied";
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ChangeShift");
        }
    }

    public class shiftChangeEmp
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string shiftType { get; set; }
        public string curFirstName { get; set; }
        public string curLastName { get; set; }
        public string newFirstName { get; set; }
        public string newLastName { get; set; }
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