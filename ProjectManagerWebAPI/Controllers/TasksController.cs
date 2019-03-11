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
    public class TasksController : ApiController
    {
        ITaskRepository repository;
        public TasksController()
        {
            repository = TaskRepository.CreateTaskRepository();
        }
        [HttpPost]
        public IHttpActionResult AddTask(Task task)
        {
            string re = repository.AddTask(task);
            return Ok(re);

        }
        [HttpPost]
        public IHttpActionResult UpdateTask(Task task)
        {
            string re = repository.UpdateTask(task);
            return Ok(re);

        }
        [HttpGet]
        public IHttpActionResult GetAllTask()
        {

            List<Task> data = repository.GetAllTask();
            return Ok(data);

        }
        [HttpGet]
        [Route("api/tasks/GetAllParentTask/{Project_ID}")]
        public IHttpActionResult GetAllParentTask(int Project_ID)
        {

            List<ParentTask> data = repository.GetAllParentTask(Project_ID);
            return Ok(data);

        }
    }
}
