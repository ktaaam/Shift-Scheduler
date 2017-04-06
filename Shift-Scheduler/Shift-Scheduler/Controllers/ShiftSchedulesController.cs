using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shift_Scheduler.Models;

namespace Shift_Scheduler.Controllers
{

    //Alex

    public class ShiftSchedulesController : Controller
    {
        private ShiftContext db = new ShiftContext();

        // GET: ShiftSchedules
        public ActionResult Index()
        {
            var shiftSchedules = db.ShiftSchedules.Include(s => s.Employees);
            return View(shiftSchedules.ToList());
        }

        // GET: ShiftSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftSchedule shiftSchedule = db.ShiftSchedules.Find(id);
            if (shiftSchedule == null)
            {
                return HttpNotFound();
            }
            return View(shiftSchedule);
        }

        // GET: ShiftSchedules/Create
        public ActionResult Create()
        {
            ViewBag.empShiftScheduleID = new SelectList(db.Employees, "employeeId", "userName");
            return View();
        }

        // POST: ShiftSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "shiftScheduleId,date,dayOfTheWeek,shiftType,empShiftScheduleID")] ShiftSchedule shiftSchedule)
        {
            if (ModelState.IsValid)
            {
                db.ShiftSchedules.Add(shiftSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.empShiftScheduleID = new SelectList(db.Employees, "employeeId", "userName", shiftSchedule.empShiftScheduleID);
            return View(shiftSchedule);
        }

        // GET: ShiftSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftSchedule shiftSchedule = db.ShiftSchedules.Find(id);
            if (shiftSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.empShiftScheduleID = new SelectList(db.Employees, "employeeId", "userName", shiftSchedule.empShiftScheduleID);
            return View(shiftSchedule);
        }

        // POST: ShiftSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "shiftScheduleId,date,dayOfTheWeek,shiftType,empShiftScheduleID")] ShiftSchedule shiftSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shiftSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empShiftScheduleID = new SelectList(db.Employees, "employeeId", "userName", shiftSchedule.empShiftScheduleID);
            return View(shiftSchedule);
        }

        // GET: ShiftSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftSchedule shiftSchedule = db.ShiftSchedules.Find(id);
            if (shiftSchedule == null)
            {
                return HttpNotFound();
            }
            return View(shiftSchedule);
        }

        // POST: ShiftSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShiftSchedule shiftSchedule = db.ShiftSchedules.Find(id);
            db.ShiftSchedules.Remove(shiftSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
