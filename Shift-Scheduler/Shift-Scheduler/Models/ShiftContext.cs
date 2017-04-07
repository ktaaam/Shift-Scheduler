using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Shift_Scheduler.Models
{
    public class ShiftContext: DbContext        
    {
        public ShiftContext():
            base("DefaultConnection")
        { }

        public DbSet<Shifts> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<ShiftSchedule> ShiftSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}