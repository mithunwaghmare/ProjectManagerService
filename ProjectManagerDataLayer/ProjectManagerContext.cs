using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using ProjectManagerEntities;
namespace ProjectManagerDataLayer
{
    public class ProjectManagerContext : DbContext
    {
        public ProjectManagerContext() :base("name=ProjectManagerContext")
        {

        }
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Project> projects { get; set; }
        public virtual DbSet<Task> tasks { get; set; }
        public virtual DbSet<ParentTask> parenttasks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<ProjectManagerContext>(null);
            
            modelBuilder.Entity<Project>()
             .Ignore(i => i.noofCompletedTasks);
            modelBuilder.Entity<Project>()
            .Ignore(i => i.noofTasks);
            modelBuilder.Entity<Project>()
           .Ignore(i => i.managerDetails);

            modelBuilder.Entity<Task>()
          .Ignore(i => i.ParentTaskName);

            //base.OnModelCreating(modelBuilder);

        }
    }
}
