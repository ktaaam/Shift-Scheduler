using Shift_Scheduler.Models;
using Shift_Scheduler.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Shift_Scheduler.Controllers
{
    public class ShiftScheduleController : Controller
    {
        ShiftContext db = new ShiftContext();

        // GET: ShiftSchedule
        public ActionResult Index()
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            DateTime endOfWeek = startOfWeek.AddDays(6);

            ViewBag.shift = db.ShiftSchedules.ToList();
            ViewBag.employee = db.Employees.ToList();

            foreach (var s in db.ShiftSchedules)
            {
                var res = ( from ss in db.ShiftSchedules
                          from e in db.Employees
                          where ss.date >= startOfWeek && ss.date <= endOfWeek && ss.empShiftScheduleID == e.employeeId
                          select new ShiftScheduleViewModel
                          {
                             dayOfTheWeek =  ss.dayOfTheWeek, shiftType = ss.shiftType, firstName = e.firstName, lastName = e.lastName
                          }).ToList();
                return View(res);
                //ViewBag.filtered = res;
            }

            return View(db.ShiftSchedules.ToList());

        }


    }
}