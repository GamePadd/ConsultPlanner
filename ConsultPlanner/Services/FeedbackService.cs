using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;

namespace ConsultPlanner.Services
{
    public interface IFeedbackService
    {
        List<Feedbacks> GetAllFeedback();
        void DeleteFeedback(int id);
    }
    public class FeedbackService : IFeedbackService
    {
        public List<Feedbacks> GetAllFeedback() 
        { 
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.Feedbacks.Include(s => s.Sessions).Include(u => u.Users).ToList();
            }
        }

        public void DeleteFeedback(int id)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    var existingFeedback = _context.Feedbacks.Find(id);

                    if(existingFeedback != null)
                    {
                        _context.Feedbacks.Remove(existingFeedback);
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
