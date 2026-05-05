using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultPlanner.Services
{
    public interface IRoleInterface
    {
        List<Roles> GetAllRoles();
    }
    public class RoleService : IRoleInterface
    {
        private ConsultPlannerEntities _context;

        public RoleService()
        {
            _context = new ConsultPlannerEntities();
        }

        public List<Roles> GetAllRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
