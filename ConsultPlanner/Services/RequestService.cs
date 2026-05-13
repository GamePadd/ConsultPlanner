using System;
using System.Collections.Generic;
using System.Linq;
using ConsultPlanner.Models;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;

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

        public List<ConsultationRequests> GetAllRequests()
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.ConsultationRequests.ToList();
            }
        }

        public List<ConsultationRequests> GetAllRequestsWithNames()
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.ConsultationRequests.Include(u => u.Users).Include(c => c.Consultants).Include(cu => cu.Consultants.Users).ToList();
            }
        }

        public void AddRequest(ConsultationRequests request)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    _context.ConsultationRequests.Add(request);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateRequest(ConsultationRequests request)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    var existingRequest = _context.ConsultationRequests.Find(request.ID);

                    if (existingRequest != null)
                    {
                        existingRequest.UserID = request.UserID;
                        existingRequest.ConsultantID = request.ConsultantID;
                        existingRequest.Description = request.Description;
                        existingRequest.Status = request.Status;
                        existingRequest.RequestDate = request.RequestDate;

                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteRequest(int requestID)
        {
            try
            {
                using (var _context = new ConsultPlannerEntities())
                {
                    var existingRequest = _context.ConsultationRequests.Find(requestID);
                    if (existingRequest != null)
                    {
                        _context.ConsultationRequests.Remove(existingRequest);
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
