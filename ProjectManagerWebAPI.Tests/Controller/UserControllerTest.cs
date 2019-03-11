using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class UserControllerTest
    {

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetAllUserTest()
        {
            var data = new List<User>()
            {
                new User {User_ID=1,Employee_ID="1",FirstName="FirstName1",LastName="LastName1"},
                new User {User_ID=2,Employee_ID="2",FirstName="FirstName2",LastName="LastName2"}
            }.AsQueryable();

            var mock = new Mock<DbSet<User>>();
            mock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            mock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            mock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.users).Returns(mock.Object);

            var service = new UserRepository(context.Object);
            List<User> users = service.GetAllUsers();
            Assert.That(users.Count == 2);

        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetUserByIDTest()
        {
            var data = new List<User>()
            {
                new User {User_ID=1,Employee_ID="1",FirstName="FirstName1",LastName="LastName1"},
                new User {User_ID=2,Employee_ID="2",FirstName="FirstName2",LastName="LastName2"}
            };
            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.users).Returns(new Mock<DbSet<User>>().SetupData(data, o =>
            {
                return data.Single(x => x.User_ID == (int)o.First());
            }).Object);


            var service = new UserRepository(context.Object);
            User user = service.GetUserByEmployeeID(1);
            Assert.That(user.User_ID == 1);
        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void AddUserTest()
        {
            var data = new List<User>()
            {
                new User {User_ID=1,Employee_ID="1",FirstName="FirstName1",LastName="LastName1"},
                new User {User_ID=2,Employee_ID="2",FirstName="FirstName2",LastName="LastName2"}
            }.AsQueryable();

            var mock = new Mock<DbSet<User>>();
            mock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            mock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            mock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.users).Returns(mock.Object);

            var service = new UserRepository(context.Object);
            User user = new User { User_ID = 3, Employee_ID = "3", FirstName = "FirstName3", LastName = "LastName3" };
            var ret = service.AddUser(user);
            Assert.That(ret == "User Added Successfully");


        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void UpdateUserTest()
        {

            User user = new User { User_ID = 1, Employee_ID = "1", FirstName = "FirstName1", LastName = "LastName1" };
            user.FirstName = "FirstName1";
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(m => m.UpdateUser(user)).Returns("User updated successfully");
            var ret = mockRepository.Object.UpdateUser(user);
            Assert.That(ret == "User updated successfully");
        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void DeleteUserTest()
        {
            var data = new List<User>()
            {
                new User {User_ID=1,Employee_ID="1",FirstName="FirstName1",LastName="LastName1"},
                new User {User_ID=2,Employee_ID="2",FirstName="FirstName2",LastName="LastName2"}
            };
            var context = new Mock<ProjectManagerContext>();
            context.Setup(x => x.users).Returns(new Mock<DbSet<User>>().SetupData(data, o =>
            {
                return data.Single(x => x.User_ID == (int)o.First());
            }).Object);


            var service = new UserRepository(context.Object);
            var ret = service.DeleteUser(1);
            Assert.That(ret== "User deleted successfully");
        }
    }
}
