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
    }
}
