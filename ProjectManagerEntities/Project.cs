using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerEntities
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int Project_ID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public int ManagerID { get; set; }
        [NotMapped]
        public int noofTasks { get; set; }
        [NotMapped]
        public int noofCompletedTasks { get; set; }
        [NotMapped]
        public string managerDetails { get; set; }

        public bool IsSuspended { get; set; }

    }
}
