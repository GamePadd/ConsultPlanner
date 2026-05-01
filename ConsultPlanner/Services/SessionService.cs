using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultPlanner.Services
{
    public interface ISessionInterface
    {
        List<Sessions> GetAllSessions();
    }

    internal class SessionService : ISessionInterface
    {
        private ConsultPlannerEntities _context;
        public SessionService()
        {
            _context = new ConsultPlannerEntities();
        }
        public List<Sessions> GetAllSessions()
        {
            return _context.Sessions.ToList();
        }
    }
}
