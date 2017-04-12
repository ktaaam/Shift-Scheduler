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
        private ShiftContext db = new ShiftContext();
        
        
        // GET: Employee
        public ActionResult Index(int id = 0)
        {
            if (id > 0)
                this.employee = db.Employees.Find(id);
            else
                return Redirect("/login");

            List<string> empShiftSchedule = new List<string>();

            foreach (ShiftSchedule shift in employee.ShiftSchedules)
            {
                empShiftSchedule.Add(shift.shiftType + " " + shift.dayOfTheWeek);
            }

            ViewData["EmpShifts"] = empShiftSchedule;

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

        public ActionResult ShiftChangeRequest(int id = 0)
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