﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shift_Scheduler.Controllers
{
    public class ShiftScheduleController : Controller
    {
        // GET: ShiftSchedule
        public ActionResult Index()
        {
            return View();
        }
    }
}