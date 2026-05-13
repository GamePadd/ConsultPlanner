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
        public List<Sessions> GetAllSessions()
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.Sessions.ToList();
            }
        }
    }
}
