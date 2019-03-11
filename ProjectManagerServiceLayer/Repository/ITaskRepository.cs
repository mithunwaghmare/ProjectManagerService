using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManagerEntities;
using ProjectManagerDataLayer;
namespace ProjectManagerServiceLayer.Repository
{
    public interface ITaskRepository
    {
        string AddTask(Task task);
        List<Task> GetAllTask();

        List<ParentTask> GetAllParentTask(int Project_ID);
        string UpdateTask(Task task);
       


    }
}
