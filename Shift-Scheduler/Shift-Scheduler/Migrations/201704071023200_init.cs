namespace Shift_Scheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        employeeId = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        passWord = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        role = c.String(),
                        address = c.String(),
                        phoneNumber = c.String(),
                        picture = c.Binary(),
                        department = c.String(),
                    })
                .PrimaryKey(t => t.employeeId);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        shiftId = c.String(nullable: false, maxLength: 128),
                        dayOfTheWeek = c.String(),
                        shiftType = c.String(),
                    })
                .PrimaryKey(t => t.shiftId);
            
            CreateTable(
                "dbo.ShiftSchedules",
                c => new
                    {
                        shiftScheduleId = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        dayOfTheWeek = c.String(),
                        shiftType = c.String(),
                        empShiftScheduleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.shiftScheduleId)
                .ForeignKey("dbo.Employees", t => t.empShiftScheduleID, cascadeDelete: true)
                .Index(t => t.empShiftScheduleID);
            
            CreateTable(
                "dbo.ShiftsEmployees",
                c => new
                    {
                        Shifts_shiftId = c.String(nullable: false, maxLength: 128),
                        Employee_employeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Shifts_shiftId, t.Employee_employeeId })
                .ForeignKey("dbo.Shifts", t => t.Shifts_shiftId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_employeeId, cascadeDelete: true)
                .Index(t => t.Shifts_shiftId)
                .Index(t => t.Employee_employeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShiftSchedules", "empShiftScheduleID", "dbo.Employees");
            DropForeignKey("dbo.ShiftsEmployees", "Employee_employeeId", "dbo.Employees");
            DropForeignKey("dbo.ShiftsEmployees", "Shifts_shiftId", "dbo.Shifts");
            DropIndex("dbo.ShiftsEmployees", new[] { "Employee_employeeId" });
            DropIndex("dbo.ShiftsEmployees", new[] { "Shifts_shiftId" });
            DropIndex("dbo.ShiftSchedules", new[] { "empShiftScheduleID" });
            DropTable("dbo.ShiftsEmployees");
            DropTable("dbo.ShiftSchedules");
            DropTable("dbo.Shifts");
            DropTable("dbo.Employees");
        }
    }
}
