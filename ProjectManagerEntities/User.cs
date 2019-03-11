using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectManagerEntities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Employee_ID { get; set; }
        
    }
}
