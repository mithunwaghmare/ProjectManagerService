namespace ProjectManagerDataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParentTasks",
                c => new
                    {
                        Parent_ID = c.Int(nullable: false, identity: true),
                        ParentTaskName = c.String(),
                    })
                .PrimaryKey(t => t.Parent_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Project_ID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        ManagerID = c.Int(nullable: false),
                        IsSuspended = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Project_ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Task_ID = c.Int(nullable: false, identity: true),
                        Parent_ID = c.Int(nullable: false),
                        Project_ID = c.Int(nullable: false),
                        TaskName = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Priority = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        IsParentTask = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Task_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Employee_ID = c.String(),
                    })
                .PrimaryKey(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
            DropTable("dbo.ParentTasks");
        }
    }
}
