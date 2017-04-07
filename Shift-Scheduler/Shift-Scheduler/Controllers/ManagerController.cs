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
            return View();
        }

        public JsonResult GetShifts()
        {
            Employee[] output = new Employee[30];

            int i = 0;

            foreach (var shift in db.Shifts)
            {
                var res = from e in db.Employees
                          from s in e.shifts
                          where s.shiftId == shift.shiftId
                          select e;
                output[i] = res.FirstOrDefault();

                i++;
            }

            return Json(output, JsonRequestBehavior.AllowGet);

        }
        public ActionResult employeeList()
        { 
            return View(db.Employees.ToList());
        }

        public ActionResult Report()
        {
            return View();
        }
    }
}