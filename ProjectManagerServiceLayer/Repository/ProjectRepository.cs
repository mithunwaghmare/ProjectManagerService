using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagerEntities;
using ProjectManagerDataLayer;
namespace ProjectManagerServiceLayer.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        ProjectManagerContext _context;
        public static ProjectRepository CreateProjectRepository()
        {
            return new ProjectRepository(new ProjectManagerContext());
        }
        public ProjectRepository(ProjectManagerContext context)
        {
            _context = context;
        }
        public string AddProject(Project project)
        {
            try
            {
                _context.projects.Add(project);
                _context.SaveChanges();
                return "Project added successfully.";
            }
            catch(Exception ex)
            {
                return "Error while adding project."; ;
            }
        }

        public List<Project> GetAllProjects()
        {
            List<Project> data = _context.projects.ToList();
            foreach(var d in data)
            {
                d.noofTasks = _context.tasks.Where(x => x.Project_ID == d.Project_ID).Count();
                d.noofCompletedTasks = _context.tasks.Where(x => x.EndDate != null && x.Project_ID == d.Project_ID).Count();
                d.managerDetails  = (from mgr in _context.users.ToList()
                                   where mgr.Employee_ID == d.ManagerID.ToString()
                                   select d.ManagerID + "-" + mgr.FirstName + " " + mgr.LastName).SingleOrDefault();


                    //d.ManagerID+"-"+ _context.users.Where(x => x.Employee_ID == d.ManagerID.ToString()).Select(x=>x.FirstName+" "+x.LastName).to.ToString();
            }
            return data;
        }

        public string UpdateProject(Project project)
        {
            var entity = _context.projects.Find(project.Project_ID);
            _context.Entry(entity).CurrentValues.SetValues(project);
            _context.SaveChanges();
            return "Project updated successfully.";
        }
    }
}
