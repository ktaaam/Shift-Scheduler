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

        public ActionResult ShiftChangeRequest(int id, string shiftId)
        {
            if (id > 0)
                this.employee = db.Employees.Find(id);
            else
                return Redirect("/login");

            var res = from e in db.Employees
                      from s in e.shifts
                      where s.shiftId == shiftId
                      select s;

            var res2 = from e in db.Employees
                       from s in e.shifts
                       where s.dayOfTheWeek != db.ShiftSchedules.Find(shiftId).dayOfTheWeek
                       select e;



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