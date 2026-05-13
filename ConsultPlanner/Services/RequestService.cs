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
        void AddRequest(ConsultationRequests request);
        void UpdateRequest(ConsultationRequests request);
        void DeleteRequest(int requestID);
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

        public void AddRequest(ConsultationRequests request)
        {

        }
        public void UpdateRequest(ConsultationRequests request)
        {

        }
        public void DeleteRequest(int requestID)
        {

        }
    }
}
