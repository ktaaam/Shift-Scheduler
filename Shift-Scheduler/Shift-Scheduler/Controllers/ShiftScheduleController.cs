using Shift_Scheduler.Models;
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

            ViewBag.shift = db.ShiftSchedules.ToList();
            ViewBag.employee = db.Employees.ToList();
            
            return View(db.ShiftSchedules.ToList());

        }


    }
}