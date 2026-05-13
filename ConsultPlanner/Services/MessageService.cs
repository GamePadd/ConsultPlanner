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
    public interface IMessageService
    {
        List<SessionMessages> GetAllMessages();
        void DeleteMessage(int id);
    }
    public class MessageService : IMessageService
    {
        public List<SessionMessages> GetAllMessages()
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.SessionMessages.Include(u => u.Users).Include(s => s.Sessions).ToList();
            }
        }

        public void DeleteMessage(int id)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    var existingMessage = _context.SessionMessages.Find(id);

                    if (existingMessage != null)
                    {
                        _context.SessionMessages.Remove(existingMessage);
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
