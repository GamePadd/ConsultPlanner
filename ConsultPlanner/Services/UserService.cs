using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsultPlanner.Services
{
    public interface IUserInterface
    {
        List<Users> GetAllUsers();
        void AddUser(Users user, List<int> roles);
        void DeleteUser(int id);
    }

    internal class UserService : IUserInterface
    {
        private ConsultPlannerEntities _context;
        public UserService()
        {
            _context = new ConsultPlannerEntities();
        }
        public List<Users> GetAllUsers() 
        {
            return _context.Users.ToList();
        }

        public void AddUser(Users user, List<int> roles)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                if (roles != null && roles.Any())
                {
                    foreach (var role in roles) {

                        var userRole = new UserRoles
                        {
                            UserID = user.ID,
                            RoleID = role
                        };
                        _context.UserRoles.Add(userRole);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
