using System;
using System.Data.Entity;

namespace Shift_Scheduler.Models
{
    public class Employee_model
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string role { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string department { get; set; }
        
    }

    public class EmployeeDBContext : DbContext
    {
        public DbSet<Employee_model> Employees { get; set; }
    }
}