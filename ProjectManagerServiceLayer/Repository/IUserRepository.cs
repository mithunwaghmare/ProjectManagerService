using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagerDataLayer;
using ProjectManagerEntities;
namespace ProjectManagerServiceLayer.Repository
{
    public interface IUserRepository 
    {
        string AddUser(User user);
        List<User> GetAllUsers();
        User GetUserByEmployeeID(int EmployeeID);
        string UpdateUser(User user);
        string DeleteUser(int User_ID);
    }
}
