using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    var sessions = _context.Sessions.Find(sessionId);
                    if (sessions != null)
                    {
                        _context.Sessions.Remove(sessions);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AddSession(Sessions session, List<int> topics)
        {
            try
            {
                using(var _context = new ConsultPlannerEntities())
                {
                    _context.Sessions.Add(session);
                    _context.SaveChanges();

                    if (topics != null)
                    {

                        foreach (var topicID in topics)
                        {
                            var newTopic = new SessionTopics
                            {
                                SessionID = session.ID,
                                TopicID = topicID
                            };

                            _context.SessionTopics.Add(newTopic);
                        }
                        _context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateSession(Sessions session, List<int> topics)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    var existingSession = _context.Sessions.Find(session.ID);

                    if (existingSession != null)
                    {
                        existingSession.Name = session.Name;
                        existingSession.Description = session.Description;
                        existingSession.RequestID = session.RequestID;
                        existingSession.Status = session.Status;
                        existingSession.StartTime = session.StartTime;
                        existingSession.EndTime = session.EndTime;

                        if (topics != null)
                        {
                            var allTopics = _context.SessionTopics.Where(t => t.SessionID == session.ID);
                            _context.SessionTopics.RemoveRange(allTopics);

                            foreach (var topicID in topics)
                            {
                                var newTopic = new SessionTopics
                                {
                                    SessionID = session.ID,
                                    TopicID = topicID
                                };

                                _context.SessionTopics.Add(newTopic);
                            }
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
    }
}
