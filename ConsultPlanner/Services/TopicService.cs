using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultPlanner.Services
{
    public interface ITopicInterface
    {
        List<Topics> GetAllTopics();
        List<int> GetAllConsultTopics(int consultantID);
        List<int> GetAllSessionTopics(int sessionID);
    }
    public class TopicService : ITopicInterface
    {

        public List<Topics> GetAllTopics()
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.Topics.ToList();
            }
        }

        public List<int> GetAllConsultTopics(int consultantID)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.ConsultantTopics.Where(r => r.ConsultantID == consultantID).Select(r => r.TopicID).ToList();
            }
        }

        public List<int> GetAllSessionTopics(int sessionID)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.SessionTopics.Where(r => r.SessionID == sessionID).Select(r => r.TopicID).ToList();
            }
        }
    }
}
