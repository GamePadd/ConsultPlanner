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
        void DeleteSession(int sessionId);
        void AddSession(Sessions session, List<int> topics);
        void UpdateSession(Sessions session, List<int> topics);
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

        public void DeleteSession(int sessionId)
        {

        }
        public void AddSession(Sessions session, List<int> topics)
        {

        }
        public void UpdateSession(Sessions session, List<int> topics)
        {

        }
    }
}
