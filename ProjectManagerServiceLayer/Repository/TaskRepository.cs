using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManagerEntities;
using ProjectManagerDataLayer;
namespace ProjectManagerServiceLayer.Repository
{
    public class TaskRepository : ITaskRepository
    {
        ProjectManagerContext _context;
        public static ITaskRepository CreateTaskRepository()
        {
            return new TaskRepository(new ProjectManagerContext());
        }
        public TaskRepository(ProjectManagerContext context)
        {
            _context = context;
        }
        public string AddTask(Task task)
        {
            int ParentID = 0;
            if (task.IsParentTask)
            {
                ParentTask ptask = new ParentTask();
                ptask.ParentTaskName = task.TaskName;
                _context.parenttasks.Add(ptask);
                _context.SaveChanges();
                ParentID = ptask.Parent_ID;
                task.Parent_ID = ParentID;
            }
            
            _context.tasks.Add(task);
            _context.SaveChanges();
            
            return "Task added successfully.";

        }

        public List<Task> GetAllTask()
        {
            var data=_context.tasks.Where(x=>x.IsParentTask==false).ToList();
            foreach(var d in data)
            {
                d.ParentTaskName= (from pt in _context.parenttasks.ToList()
                                    where pt.Parent_ID == d.Parent_ID
                                    select pt.ParentTaskName).FirstOrDefault();

                d.ProjectName = (from pt in _context.projects.ToList()
                                    where pt.Project_ID == d.Project_ID
                                    select pt.ProjectName).FirstOrDefault();

                d.UserName = (from pt in _context.users.ToList()
                                 where pt.Employee_ID == d.User_ID.ToString()
                                 select pt.FirstName +" "+pt.LastName).FirstOrDefault();

            }
            return data;
        }

        public List<ParentTask> GetAllParentTask(int Project_ID)
        {
            var data = (from t in _context.tasks.ToList()
                        join pt in _context.parenttasks.ToList()
                        on t.Parent_ID equals pt.Parent_ID
                        where t.IsParentTask==true && t.Project_ID==Project_ID
                        select new ParentTask
                        {
                            Parent_ID = pt.Parent_ID,
                            ParentTaskName = pt.ParentTaskName

                        }).ToList();
            return data;
        }

        public string UpdateTask(Task task)
        {
            var entity = _context.tasks.Find(task.Task_ID);
            _context.Entry(entity).CurrentValues.SetValues(task);
            _context.SaveChanges();

            if(task.IsParentTask)
            {
                var parentity = _context.parenttasks.Find(task.Parent_ID);
                parentity.ParentTaskName = task.TaskName;
                //_context.Entry(parentity).CurrentValues.SetValues(parentity);
                _context.SaveChanges();
            }

            return "Task updated successfully."; 
        }
    }
}
