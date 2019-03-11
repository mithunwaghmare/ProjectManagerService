using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagerEntities;
using ProjectManagerDataLayer;
namespace ProjectManagerServiceLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        ProjectManagerContext _context;
        public static IUserRepository CreateUserRepository()
        {
            return new UserRepository(new ProjectManagerContext());
        }
        public UserRepository(ProjectManagerContext context)
        {
            _context = context;
        }
        public string AddUser(User user)
        {
            try
            {
                if (_context.users.Any(o => o.Employee_ID == user.Employee_ID)) return "Emp ID alredy exist";


                _context.users.Add(user);
                _context.SaveChanges();
                return "User Added Successfully";
            }
            catch(Exception ex)
            {
                return "Error while adding user"; 
            }
        }

        public List<User> GetAllUsers()
        {
            return _context.users.ToList();
        }

        public User GetUserByEmployeeID(int EmployeeID)
        {
            return _context.users.Single(x=>x.Employee_ID==EmployeeID.ToString());
        }

        public string UpdateUser(User user)
        {
            var entity = _context.users.Find(user.User_ID);
            _context.Entry(entity).CurrentValues.SetValues(user);
            _context.SaveChanges();
            return "User updated successfully";
        }

        public string DeleteUser(int User_ID)
        {
            var entity = _context.users.Find(User_ID);
            _context.users.Remove(entity);
            _context.SaveChanges();
            return "User deleted successfully";
        }
    }
}
