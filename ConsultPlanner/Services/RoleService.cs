using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsultPlanner.Services
{
    public interface IRoleInterface
    {
        List<Roles> GetAllRoles();
        List<int> GetAllRolesID(int userID);
        void AddRole(Roles role);
        void UpdateRole(Roles role);
        void DeleteRole(int roleID);
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

        public void AddRole(Roles role)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                try
                {
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void UpdateRole(Roles role)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                try
                {
                    var existingRole = _context.Roles.Find(role.ID);

                    if(existingRole != null)
                    {
                        existingRole.Name = role.Name;
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void DeleteRole(int roleID)
        {
            using (var _context = new ConsultPlannerEntities())
            {
                try
                {
                    var existingRole = _context.Roles.Find(roleID);

                    if (existingRole != null)
                    {
                        _context.Roles.Remove(existingRole);
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
}
