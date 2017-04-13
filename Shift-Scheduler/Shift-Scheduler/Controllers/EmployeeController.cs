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
        private Employee employee;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            var sessionToken = Session["LoginSession"];
            if (sessionToken != null)
                this.employee = db.Employees.Find(sessionToken);
            else
                return RedirectToAction("Login", "Account");


            if (employee == null)
                return RedirectToAction("login", "Account");

            ViewData["EmpShifts"] = employee.shiftSchedules;
            ViewData["EmpName"] = employee.firstName + " " + employee.lastName;
            ViewData["LoginSession"] = employee.Email;
            return View();
        }

        public ActionResult UserProfile()
        {
            if (Session["LoginSession"] != null)
                this.employee = db.Employees.Find(Session["LoginSession"]);
            else
                return RedirectToAction("Login", "Account");


            ViewData["EmpName"] = employee.firstName + " " + employee.lastName;
            ViewData["LoginSession"] = employee.Email;
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile([Bind(Include = "employeeId,userName,passWord,firstName,lastName,role,address,phoneNumber,department")] Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["LoginSession"] = employee.Email;
            return View(employee);
        }

        public ActionResult ShiftChangeRequest()
        {
            if (Session["LoginSession"] != null)
                this.employee = db.Employees.Find(Session["LoginSession"]);
            else
                return RedirectToAction("Login","Account"); 

            var res = (from s in db.ShiftSchedules
                       from c in db.Shifts
                       from e in c.employee
                       where s.empShiftScheduleID == employee.employeeId && c.shiftType == s.shiftType && c.dayOfTheWeek == s.dayOfTheWeek && s.shiftScheduleId == employee.employeeId && e.employeeId != employee.employeeId
                       select e).ToList();

            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["ShiftSchedule"] = employee.employeeId;
            ViewData["Employees"] = res;
            return View();
        }

        [HttpPost]
        public ActionResult ShiftChangeRequest(int shiftscheduleid, int new_emp)
        {
            if (Session["LoginSession"] != null)
                this.employee = db.Employees.Find(Session["LoginSession"]);
            else
                return RedirectToAction("Login", "Account");

            var res = (from e in db.Employees
                       from s in e.shiftSchedules
                       where e.employeeId == employee.employeeId && s.shiftScheduleId == shiftscheduleid
                       select s).FirstOrDefault();

            if (res != null)
            {
                ShiftChangeRequest req = new ShiftChangeRequest();

                req.shiftApproval = "pending";
                req.newWorkingEmp = (from e in db.Employees
                                     where e.employeeId == new_emp
                                     select e).FirstOrDefault();

                req.currentWorkingEmp = (from e in db.Employees
                                         where e.employeeId == employee.employeeId
                                         select e).FirstOrDefault();

                req.shiftSchedule = res;
                req.shiftScheduleID = res.shiftScheduleId;

                db.shiftChangeRequest.Add(req);
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        public ActionResult VacationRequest()
        {
            if (Session["LoginSession"] != null)
                this.employee = db.Employees.Find(Session["LoginSession"]);
            else
                return RedirectToAction("Login", "Account");

            if (employee == null)
                return HttpNotFound();

            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["LoginSession"] = employee.Email;
            return View();
        }

        public ActionResult ClockIn()
        {
            if (Session["LoginSession"] != null)
                this.employee = db.Employees.Find(Session["LoginSession"]);
            else
                return RedirectToAction("Login", "Account");

            var res = (from c in db.Clocks
                       where c.empClockID == employee.employeeId && c.clockOut == null && c.clockIn != null
                       select c).FirstOrDefault();

            if (res == null)
            {
                Clock clockin = new Clock();
                clockin.Employees = (from e in db.Employees
                                     where e.employeeId == employee.employeeId
                                     select e).FirstOrDefault();
                clockin.empClockID = employee.employeeId;
                clockin.clockIn = DateTime.Now;

                db.Clocks.Add(clockin);
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        public ActionResult ClockOut()
        {
            if (Session["LoginSession"] != null)
                this.employee = db.Employees.Find(Session["LoginSession"]);
            else
                return RedirectToAction("Login", "Account");

            var res = (from c in db.Clocks
                       where c.empClockID == employee.employeeId && c.clockIn != null && c.clockOut == null
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.clockOut = DateTime.Now;

                db.Entry(res).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}