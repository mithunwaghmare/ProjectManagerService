using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerEntities
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public int Task_ID { get; set; }
        public int Parent_ID { get; set; }
        public int Project_ID { get; set; }
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public int User_ID { get; set; }
        public bool IsParentTask { get; set; }
        [NotMapped]
        public string ParentTaskName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string ProjectName { get; set; }

    }
}
