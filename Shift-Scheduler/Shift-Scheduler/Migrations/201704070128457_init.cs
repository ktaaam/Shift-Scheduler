namespace Shift_Scheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "phoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "phoneNumber", c => c.Long(nullable: false));
        }
    }
}
