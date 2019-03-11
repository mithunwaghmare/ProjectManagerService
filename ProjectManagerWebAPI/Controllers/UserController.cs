using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManagerServiceLayer.Repository;
using ProjectManagerEntities;
namespace ProjectManagerWebAPI.Controllers
{
    public class UserController : ApiController
    {
        IUserRepository repository;
        public UserController()
        {
            repository = UserRepository.CreateUserRepository();
        }
        [HttpPost]
        public IHttpActionResult AddUser(User user)
        {
            string message = repository.AddUser(user);
            return Ok(message);

        }
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {

            List<User> data = repository.GetAllUsers();
            return Ok(data);

        }
        [HttpGet]
        [Route("api/user/GetUserByID/{EmployeeID}")]
        public IHttpActionResult GetUserByID(int EmployeeID)
        {
            User data = repository.GetUserByEmployeeID(EmployeeID);
            return Ok(data);
        }

        [HttpPost]
        public IHttpActionResult UpdateUser(User user)
        {
            string re = repository.UpdateUser(user);
            return Ok(re);

        }
        [HttpDelete]
        [Route("api/user/DeleteUser/{UserID}")]
        public IHttpActionResult DeleteTask(int UserID)
        {
            string re = repository.DeleteUser(UserID);
            return Ok(re);

        }

    }
}
