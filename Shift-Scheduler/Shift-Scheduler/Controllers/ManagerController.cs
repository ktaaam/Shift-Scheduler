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
            IQueryable<Employee>[] output = new IQueryable<Employee>[30];            

            int i = 0;

            foreach (var shift in db.Shifts)
            {                
                output[0] = from e in db.Employees
                            where e.shifts.Any(s => s.shiftId == shift.shiftId)
                            select e;
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