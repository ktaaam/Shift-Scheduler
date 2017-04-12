using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Shift_Scheduler.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Employee employee { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("defaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Shifts> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<ShiftSchedule> ShiftSchedules { get; set; }

        public DbSet<Vacation> VacationRequests { get; set; }

        public DbSet<ShiftChangeRequest> shiftChangeRequest { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}