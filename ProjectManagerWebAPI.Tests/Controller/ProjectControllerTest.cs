using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NBench;
using Moq;
using ProjectManagerEntities;
using ProjectManagerServiceLayer.Repository;
using ProjectManagerDataLayer;
using System.Data.Entity;
using EntityFramework.Testing;
namespace ProjectManagerWebAPI.Tests.Controller
{
    [TestFixture]
    public class ProjectControllerTest
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetAllProjectTest()
        {
            var pdata = new List<Project>()
            {
                new Project {Project_ID=1,ProjectName="TestProject1",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,ManagerID=1,IsSuspended=true,noofTasks=1,noofCompletedTasks=1,managerDetails="Manager1"},
                new Project {Project_ID=2,ProjectName="TestProject2",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=11,ManagerID=2,IsSuspended=false,noofTasks=1,noofCompletedTasks=1,managerDetails="Manager1"}
            }.AsQueryable();

            var mock = new Mock<DbSet<Project>>();
            mock.As<IQueryable<Project>>().Setup(x => x.Provider).Returns(pdata.Provider);
            mock.As<IQueryable<Project>>().Setup(x => x.Expression).Returns(pdata.Expression);
            mock.As<IQueryable<Project>>().Setup(x => x.ElementType).Returns(pdata.ElementType);
            mock.As<IQueryable<Project>>().Setup(x => x.GetEnumerator()).Returns(pdata.GetEnumerator());


            var udata = new List<User>()
            {
                new User {User_ID=1,Employee_ID="1",FirstName="FirstName1",LastName="LastName1"},
                new User {User_ID=2,Employee_ID="2",FirstName="FirstName2",LastName="LastName2"}
            }.AsQueryable();

            var umock = new Mock<DbSet<User>>();
            umock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(udata.Provider);
            umock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(udata.Expression);
            umock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(udata.ElementType);
            umock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(udata.GetEnumerator());


            var tdata = new List<Task>()
            {
                new Task {Task_ID=1,Parent_ID=1,Project_ID=1,TaskName="TestTask1",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,User_ID=1,IsParentTask=true,ParentTaskName="ParentTask1",UserName="FirstName1",ProjectName="TestProject1"},
                new Task {Task_ID=2,Parent_ID=1,Project_ID=1,TaskName="TestTask2",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,User_ID=1,IsParentTask=true,ParentTaskName="ParentTask1",UserName="FirstName1",ProjectName="TestProject1"}
            }.AsQueryable();

            var tmock = new Mock<DbSet<Task>>();
            tmock.As<IQueryable<Task>>().Setup(x => x.Provider).Returns(tdata.Provider);
            tmock.As<IQueryable<Task>>().Setup(x => x.Expression).Returns(tdata.Expression);
            tmock.As<IQueryable<Task>>().Setup(x => x.ElementType).Returns(tdata.ElementType);
            tmock.As<IQueryable<Task>>().Setup(x => x.GetEnumerator()).Returns(tdata.GetEnumerator());


            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.projects).Returns(mock.Object);
            context.Setup(x => x.users).Returns(umock.Object);
            context.Setup(x => x.tasks).Returns(tmock.Object);

            var service = new ProjectRepository(context.Object);
            List<Project> users = service.GetAllProjects();
            Assert.That(users.Count == 2);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void AddProjectTest()
        {
            var pdata = new List<Project>()
            {
                new Project {Project_ID=1,ProjectName="TestProject1",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,ManagerID=1,IsSuspended=true,noofTasks=1,noofCompletedTasks=1,managerDetails="Manager1"},
                new Project {Project_ID=2,ProjectName="TestProject2",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=11,ManagerID=2,IsSuspended=false,noofTasks=1,noofCompletedTasks=1,managerDetails="Manager1"}
            }.AsQueryable();

            var mock = new Mock<DbSet<Project>>();
            mock.As<IQueryable<Project>>().Setup(x => x.Provider).Returns(pdata.Provider);
            mock.As<IQueryable<Project>>().Setup(x => x.Expression).Returns(pdata.Expression);
            mock.As<IQueryable<Project>>().Setup(x => x.ElementType).Returns(pdata.ElementType);
            mock.As<IQueryable<Project>>().Setup(x => x.GetEnumerator()).Returns(pdata.GetEnumerator());


            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.projects).Returns(mock.Object);

            var service = new ProjectRepository(context.Object);
            Project project = new Project { Project_ID = 3, ProjectName = "TestProject3", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Priority = 10, ManagerID = 1, IsSuspended = true, noofTasks = 1, noofCompletedTasks = 1, managerDetails = "Manager1" };
            var ret = service.AddProject(project);
            Assert.That(ret == "Project added successfully.");

        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void UpdateProjectTest()
        {

            Project project = new Project { Project_ID = 3, ProjectName = "TestProject3", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Priority = 10, ManagerID = 1, IsSuspended = true, noofTasks = 1, noofCompletedTasks = 1, managerDetails = "Manager1" };
            project.ProjectName = "TestProject4";
            var mockRepository = new Mock<IProjectRepository>();
            mockRepository.Setup(m => m.UpdateProject(project)).Returns("Project updated successfully.");
            var ret = mockRepository.Object.UpdateProject(project);
            Assert.That(ret == "Project updated successfully.");
        }


    }
}
