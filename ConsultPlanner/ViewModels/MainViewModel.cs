using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultPlanner.ViewModels
{
    public class MainViewModel
    {
        private ConsultPlannerEntities _context = new ConsultPlannerEntities();

        public List<Users> AllUsers
        {
            get 
            {
                return _context.Users.ToList();
            }
        }

        public List<Sessions> AllSessions
        {
            get
            {
                return _context.Sessions.ToList();
            }
        }
    }
}
