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
        private EmployeeDBContext db = new EmployeeDBContext();

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View("Profile");
        }
    }
}