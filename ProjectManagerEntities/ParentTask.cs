using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectManagerEntities
{
    public class ParentTask
    {
        [Key]
        public int Parent_ID { get; set; }
        public string ParentTaskName { get; set; }
    }
}
