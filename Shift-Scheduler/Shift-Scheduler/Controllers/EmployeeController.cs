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
        public ActionResult Index(int id = 0)
        {
            if (id > 0)
                this.employee = db.Employees.Find(id);
            else
                return RedirectToAction("login","Account");

            
            if(employee == null)
                return RedirectToAction("login", "Account");

            ViewData["EmpShifts"] = employee.shiftSchedules;
            ViewData["EmpName"] = employee.firstName + " " + employee.lastName ;
            ViewData["EmpId"] = employee.employeeId;
            return View();
        }

        public ActionResult UserProfile(int? id)
        {
            if (id > 0)
                this.employee = db.Employees.Find(id);
            else
                return Redirect("/login");
            

            ViewData["EmpName"] = employee.firstName + " " + employee.lastName;
            ViewData["EmpId"] = employee.employeeId;
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
            ViewData["EmpId"] = employee.employeeId;
            return View(employee);
        }

        public ActionResult ShiftChangeRequest(int id)
        {
            int empid = 1;

            if (empid > 0)
                this.employee = db.Employees.Find(empid);
            else
                return Redirect("/login");

            var res = (from s in db.ShiftSchedules
                       from c in db.Shifts
                       from e in c.employee
                       where s.empShiftScheduleID == empid && c.shiftType == s.shiftType && c.dayOfTheWeek == s.dayOfTheWeek && s.shiftScheduleId == id && e.employeeId != empid
                       select e).ToList();

            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["ShiftSchedule"] = id;
            ViewData["Employees"] = res;
            return View();
        }

        [HttpPost]
        public ActionResult ShiftChangeRequest(int shiftscheduleid, int new_emp)
        {
            int empid = 1;
            var res = (from e in db.Employees
                       from s in e.shiftSchedules
                       where e.employeeId == empid && s.shiftScheduleId == shiftscheduleid
                       select s).FirstOrDefault();

            if(res != null)
            {
                ShiftChangeRequest req = new ShiftChangeRequest();

                req.shiftApproval = "pending";
                req.newWorkingEmp = (from e in db.Employees
                                     where e.employeeId == new_emp
                                     select e).FirstOrDefault();

                req.currentWorkingEmp = (from e in db.Employees
                                         where e.employeeId == empid
                                         select e).FirstOrDefault();

                req.shiftSchedule = res;
                req.shiftScheduleID = res.shiftScheduleId;

                db.shiftChangeRequest.Add(req);
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        public ActionResult VacationRequest(int id = 0)
        {
            if (id > 0)
                this.employee = db.Employees.Find(id);
            else
                return Redirect("/login");

            if (employee == null)
                return HttpNotFound();

            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["EmpId"] = employee.employeeId;
            return View();
        }

        public ActionResult ClockIn()
        {
            int empid = 1;            

            var res = (from c in db.Clocks
                       where c.empClockID == empid && c.clockOut == null && c.clockIn != null
                       select c).FirstOrDefault();

            if(res == null)
            {
                Clock clockin = new Clock();
                clockin.Employees = (from e in db.Employees
                                     where e.employeeId == empid
                                     select e).FirstOrDefault();
                clockin.empClockID = empid;
                clockin.clockIn = DateTime.Now;

                db.Clocks.Add(clockin);
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        public ActionResult ClockOut()
        {
            int empid = 1;

            var res = (from c in db.Clocks
                       where c.empClockID == empid && c.clockIn != null && c.clockOut == null
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