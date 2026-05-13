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
        void UpdateUser(Users user, List<int> roles);
        void DeleteUser(int id);
    }

    internal class UserService : IUserInterface
    {
        public List<Users> GetAllUsers() 
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.Users.ToList();
            }
        }

        public void AddUser(Users user, List<int> roles)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    if (roles != null && roles.Any())
                    {
                        foreach (var role in roles)
                        {

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateUser(Users user, List<int> roles)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    var existingUser = _context.Users.Find(user.ID);

                    if (existingUser != null)
                    {
                        existingUser.LastName = user.LastName;
                        existingUser.FirstName = user.FirstName;
                        existingUser.Patronymic = user.Patronymic;
                        existingUser.Birthday = user.Birthday;
                        existingUser.Phone = user.Phone;
                        existingUser.Email = user.Email;
                        existingUser.Password = user.Password;

                        if (roles != null)
                        {
                            var oldRoles = _context.UserRoles.Where(r => r.UserID == user.ID);
                            _context.UserRoles.RemoveRange(oldRoles);

                            foreach (var role in roles)
                            {
                                var userRole = new UserRoles
                                {
                                    UserID = user.ID,
                                    RoleID = role
                                };
                                _context.UserRoles.Add(userRole);
                            }
                        }
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
                using (var _context = new ConsultPlannerEntities())
                {
                    var user = _context.Users.Find(id);
                    if (user != null)
                    {
                        _context.Users.Remove(user);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
