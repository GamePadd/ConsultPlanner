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
        void AddUser(Users user);
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

        public void AddUser(Users user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
