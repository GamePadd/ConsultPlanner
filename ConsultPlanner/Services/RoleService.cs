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
        List<int> GetAllRolesID(int userID);
    }
    public class RoleService : IRoleInterface
    {

        public List<Roles> GetAllRoles()
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.Roles.ToList();
            }
        }

        public List<int> GetAllRolesID(int userID)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                return _context.UserRoles.Where(r => r.UserID == userID).Select(r => r.RoleID).ToList();
            }
        }
    }
}
