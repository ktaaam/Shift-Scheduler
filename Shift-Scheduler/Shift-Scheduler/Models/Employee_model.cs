using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shift_Scheduler.Models
{
    public class Employee_model
    {

        [Key]
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 6)]
        [Required]
        public string username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string password { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string fname { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string lname { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string role { get; set; }

        public string address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
        public string department { get; set; }
        
    }

    public class EmployeeDBContext : DbContext
    {
        public DbSet<Employee_model> Employees { get; set; }
    }
}