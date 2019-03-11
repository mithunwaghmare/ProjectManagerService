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
    public class TasksControllerTest
    {

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetAllTaskTest()
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
                new Task {Task_ID=1,Parent_ID=1,Project_ID=1,TaskName="TestTask1",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,User_ID=1,IsParentTask=false,ParentTaskName="ParentTask1",UserName="FirstName1",ProjectName="TestProject1"},
                new Task {Task_ID=2,Parent_ID=1,Project_ID=1,TaskName="TestTask2",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,User_ID=1,IsParentTask=false,ParentTaskName="ParentTask1",UserName="FirstName1",ProjectName="TestProject1"}
            }.AsQueryable();

            var tmock = new Mock<DbSet<Task>>();
            tmock.As<IQueryable<Task>>().Setup(x => x.Provider).Returns(tdata.Provider);
            tmock.As<IQueryable<Task>>().Setup(x => x.Expression).Returns(tdata.Expression);
            tmock.As<IQueryable<Task>>().Setup(x => x.ElementType).Returns(tdata.ElementType);
            tmock.As<IQueryable<Task>>().Setup(x => x.GetEnumerator()).Returns(tdata.GetEnumerator());


            var ptdata = new List<ParentTask>()
            {
                new ParentTask {Parent_ID=1,ParentTaskName="ParentTask1" },
                new ParentTask {Parent_ID=2,ParentTaskName="ParentTask2"}
            }.AsQueryable();

            var ptmock = new Mock<DbSet<ParentTask>>();
            ptmock.As<IQueryable<ParentTask>>().Setup(x => x.Provider).Returns(ptdata.Provider);
            ptmock.As<IQueryable<ParentTask>>().Setup(x => x.Expression).Returns(ptdata.Expression);
            ptmock.As<IQueryable<ParentTask>>().Setup(x => x.ElementType).Returns(ptdata.ElementType);
            ptmock.As<IQueryable<ParentTask>>().Setup(x => x.GetEnumerator()).Returns(ptdata.GetEnumerator());


            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.projects).Returns(mock.Object);
            context.Setup(x => x.users).Returns(umock.Object);
            context.Setup(x => x.tasks).Returns(tmock.Object);
            context.Setup(x => x.parenttasks).Returns(ptmock.Object);

            var service = new TaskRepository(context.Object);
            List<Task> tasks = service.GetAllTask();
            Assert.That(tasks.Count == 2);



        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void AddTaskTest()
        {
            var tdata = new List<Task>()
            {
                new Task {Task_ID=1,Parent_ID=1,Project_ID=1,TaskName="TestTask1",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,User_ID=1,IsParentTask=false,ParentTaskName="ParentTask1",UserName="FirstName1",ProjectName="TestProject1"},
                new Task {Task_ID=2,Parent_ID=1,Project_ID=1,TaskName="TestTask2",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(1),Priority=10,User_ID=1,IsParentTask=false,ParentTaskName="ParentTask1",UserName="FirstName1",ProjectName="TestProject1"}
            }.AsQueryable();

            var tmock = new Mock<DbSet<Task>>();
            tmock.As<IQueryable<Task>>().Setup(x => x.Provider).Returns(tdata.Provider);
            tmock.As<IQueryable<Task>>().Setup(x => x.Expression).Returns(tdata.Expression);
            tmock.As<IQueryable<Task>>().Setup(x => x.ElementType).Returns(tdata.ElementType);
            tmock.As<IQueryable<Task>>().Setup(x => x.GetEnumerator()).Returns(tdata.GetEnumerator());

            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.tasks).Returns(tmock.Object);
            var service = new TaskRepository(context.Object);

            Task task = new Task { Task_ID = 3, Parent_ID = 1, Project_ID = 1, TaskName = "TestTask3", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Priority = 10, User_ID = 1, IsParentTask = false, ParentTaskName = "ParentTask1", UserName = "FirstName1", ProjectName = "TestProject1" };

            string ret=service.AddTask(task);

            Assert.That(ret == "Task added successfully.");

        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void UpdateTaskTest()
        {
            Task task = new Task { Task_ID = 3, Parent_ID = 1, Project_ID = 1, TaskName = "TestTask3", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Priority = 10, User_ID = 1, IsParentTask = false, ParentTaskName = "ParentTask1", UserName = "FirstName1", ProjectName = "TestProject1" };
            task.TaskName = "TestTask4";
            
            var mockRepository = new Mock<ITaskRepository>();
            mockRepository.Setup(m => m.UpdateTask(task)).Returns("Task updated successfully.");
            var ret = mockRepository.Object.UpdateTask(task);
            Assert.That(ret == "Task updated successfully.");
        }


    }
}
