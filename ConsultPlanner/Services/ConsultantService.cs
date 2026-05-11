using System;
using System.Collections.Generic;
using ConsultPlanner.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;

namespace ConsultPlanner.Services
{
    public interface IConsultantService
    {
        List<Consultants> GetAllConsultants();
        List<Consultants> GetAllConsultantsWithNames();
        void AddConsultant(Consultants consultant, List<int> topics);
        void UpdateConsultant(Consultants consultant, List<int> topics);
        void DeleteConsultant(int id);
    }
    public class ConsultantService : IConsultantService
    {
        private ConsultPlannerEntities _context;

        public ConsultantService()
        {
            _context = new ConsultPlannerEntities();
        }
        public List<Consultants> GetAllConsultants()
        {
            return _context.Consultants.ToList();
        }

        public List<Consultants> GetAllConsultantsWithNames()
        {
            return _context.Consultants.Include(c => c.Users).ToList();
        }
        public void DeleteConsultant(int id)
        {
            try
            {
                var consultant = _context.Consultants.Find(id);
                if (consultant != null)
                {
                    _context.Consultants.Remove(consultant);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddConsultant(Consultants consultant, List<int> topics)
        {
            try
            {
                _context.Consultants.Add(consultant);
                _context.SaveChanges();

                if (topics != null)
                {
                    foreach(var topic in topics)
                    {
                        var consTopic = new ConsultantTopics
                        {
                            ConsultantID = consultant.ID,
                            TopicID = topic
                        };
                        _context.ConsultantTopics.Add(consTopic);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateConsultant(Consultants consultant, List<int> topics)
        {
            try
            {
                var existingConsultant = _context.Consultants.Find(consultant.ID);
                if (existingConsultant != null)
                {
                    existingConsultant.UserID = consultant.UserID;
                    existingConsultant.Description = consultant.Description;
                    existingConsultant.Experience = consultant.Experience;

                    if (topics != null)
                    {
                        var oldTopics = _context.ConsultantTopics.Where(i => i.ConsultantID == consultant.ID);
                        _context.ConsultantTopics.RemoveRange(oldTopics);

                        foreach (var topic in topics)
                        {
                            var consTopic = new ConsultantTopics
                            {
                                ConsultantID = consultant.ID,
                                TopicID = topic
                            };
                            _context.ConsultantTopics.Add(consTopic);
                        }
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
