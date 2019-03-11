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
    public class ProjectsController : ApiController
    {
        IProjectRepository repository;
        public ProjectsController()
        {
            repository = ProjectRepository.CreateProjectRepository();
        }
        [HttpPost]
        public IHttpActionResult AddProject(Project project)
        {
            string re = repository.AddProject(project);
            return Ok(re);

        }
        [HttpGet]
        public IHttpActionResult GetAllProject()
        {

            List<Project> data = repository.GetAllProjects();
            return Ok(data);

        }
        [HttpPost]
        public IHttpActionResult UpdateProject(Project project)
        {
            string re = repository.UpdateProject(project);
            return Ok(re);

        }
    }
}
