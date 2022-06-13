using SanctionScanner.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanctionScanner.BAL.Service.UserRoles
{
    public class UserRolesService : IUserRolesService
    {
        private UnitOfWork uow;
        public UserRolesService()
        {
            uow = new UnitOfWork();
        }
        public bool AddUserRole(UserRoleDto data)
        {
            bool result = false;

            DAL.Model.Entity.UserRoles foundUserRoles = uow.Repository<DAL.Model.Entity.UserRoles>().GetBy(x => x.Id == data.Id).FirstOrDefault();
            if (foundUserRoles == null)
            {
                DAL.Model.Entity.UserRoles model = new DAL.Model.Entity.UserRoles
                {
                   RoleId=data.RoleId,
                    UserId = data.UserId,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Name = data.Name
                    
                };
                uow.Repository<DAL.Model.Entity.UserRoles>().Add(model);
                uow.SaveChanges();

                result = true;
            }
            return result;
        }

        public bool DeleteUserRole(int id)
        {
            var role = uow.Repository<DAL.Model.Entity.UserRoles>().GetById(id);

            if (role == null)
                return false;

            uow.Repository<DAL.Model.Entity.UserRoles>().Delete(id);
            uow.SaveChanges();
            return true;
        }

        public List<string> GetUserRoles(int id)
        {
            IList<string> roles=   new List<string>();
            return uow.Repository<DAL.Model.Entity.UserRoles>()
                           .GetBy(x => x.UserId == id)
                           .Select(x => x.Roles.Name).ToList();
        }

        public List<UserRoleDto> GetUsersRoles()
        {
            return uow.Repository<DAL.Model.Entity.UserRoles>()
                           .All()
                           .Select(x => new UserRoleDto()
                           {
                               Id = x.Id,
                               CreatedDate = (DateTime)x.CreatedDate,
                               UpdatedDate = (DateTime)x.UpdatedDate,
                               RoleId = x.RoleId,
                               UserId = x.UserId

                           }).ToList();
        }

        public bool UpdateUserRole(UserRoleDto data)
        {
            throw new NotImplementedException();
        }

        public bool UserRoleExists(int userId, int roleId)
        {
            bool result = false;

            try
            {
                DAL.Model.Entity.UserRoles role = uow.Repository<DAL.Model.Entity.UserRoles>().GetBy(x => x.RoleId == roleId && x.UserId == userId && x.IsActive==true).FirstOrDefault();

                if (role != null)
                    result = true;

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
