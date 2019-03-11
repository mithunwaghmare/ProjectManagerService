using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagerEntities;
namespace ProjectManagerServiceLayer.Repository
{
    public interface IProjectRepository
    {
        string AddProject(Project project);
        List<Project> GetAllProjects();

        string UpdateProject(Project project);

    }
}
