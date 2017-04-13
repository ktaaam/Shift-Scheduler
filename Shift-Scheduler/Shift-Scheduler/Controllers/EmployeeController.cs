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
            Session["EmpId"] = 1;
            if (Session["EmpId"] != null)
                this.employee = db.Employees.Find(Session["EmpId"]);
            else
                return RedirectToAction("Login","Account");            
            
            ViewData["EmpShifts"] = employee.shiftSchedules;

            ViewData["EmpName"] = employee.firstName + " " + employee.lastName ;
            ViewData["EmpId"] = employee.employeeId;
            return View();
        }

        public ActionResult UserProfile(int? id)
        {
            if (Session["EmpId"] != null)
                this.employee = db.Employees.Find(Session["EmpId"]);
            else
                return RedirectToAction("Login","Account");


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
                return RedirectToAction("Login","Account");

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
            if (Session["EmpId"] != null)
                this.employee = db.Employees.Find(Session["EmpId"]);
            else
                return RedirectToAction("Login","Account");

            if (employee == null)
                return HttpNotFound();

            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["EmpId"] = employee.employeeId;
            return View();
        }

        public ActionResult ClockIn(int id)
        {

            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["EmpId"] = employee.employeeId;
            return View();
        }

        public ActionResult ClockOut(int id)
        {

            ViewData["EmployeeName"] = employee.firstName + " " + employee.lastName;
            ViewData["EmpId"] = employee.employeeId;
            return View();
        }
    }
}