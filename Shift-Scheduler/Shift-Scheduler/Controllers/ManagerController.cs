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

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Shift()
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

            ViewBag.shifts = output;

            return View();
        }

        public ActionResult Report()
        {
            return View();
        }
    }
}