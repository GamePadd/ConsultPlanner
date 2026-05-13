using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsultPlanner.Services
{
    public interface ITopicInterface
    {
        List<Topics> GetAllTopics();
        List<int> GetAllConsultTopics(int consultantID);
        List<int> GetAllSessionTopics(int sessionID);
        void AddTopic(Topics topic);
        void UpdateTopic(Topics topic);
        void DeleteTopic(int topicID);
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

        public void AddTopic(Topics topic)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                try
                {
                    _context.Topics.Add(topic);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void UpdateTopic(Topics topic)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                try
                {
                    var existingTopic = _context.Topics.Find(topic.ID);

                    if (existingTopic != null)
                    {
                        existingTopic.Name = topic.Name;
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void DeleteTopic(int topicID)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                try
                {
                    var existingTopic = _context.Topics.Find(topicID);

                    if (existingTopic != null)
                    {
                        _context.Topics.Remove(existingTopic);
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
}
