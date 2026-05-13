using System;
using System.Collections.Generic;
using System.Linq;
using ConsultPlanner.Models;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ConsultPlanner.Services
{
    public interface IRequestInterface
    {
        List<ConsultationRequests> GetAllRequests();
        List<ConsultationRequests> GetAllRequestsWithNames();
    }
    public class RequestService : IRequestInterface
    {
        private ConsultPlannerEntities _context;

        public RequestService()
        {
            _context = new ConsultPlannerEntities();
        }

        public List<ConsultationRequests> GetAllRequests()
        {
            return _context.ConsultationRequests.ToList();
        }

        public List<ConsultationRequests> GetAllRequestsWithNames()
        {
            return _context.ConsultationRequests.Include(u => u.Users).Include(c => c.Consultants).Include(cu => cu.Consultants.Users).ToList();
        }
    }
}
