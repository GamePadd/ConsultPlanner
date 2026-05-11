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
    }
    public class TopicService : ITopicInterface
    {
        private ConsultPlannerEntities _context;

        public TopicService()
        {
            _context = new ConsultPlannerEntities();
        }

        public List<Topics> GetAllTopics()
        {
            return _context.Topics.ToList();
        }

        public List<int> GetAllConsultTopics(int consultantID)
        {
            return _context.ConsultantTopics.Where(r => r.ConsultantID == consultantID).Select(r => r.TopicID).ToList();
        }
    }
}
