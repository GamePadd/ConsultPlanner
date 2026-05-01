using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultPlanner.Services
{
    public interface IUserInterface
    {
        List<Users> GetAllUsers();
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
    }
}
