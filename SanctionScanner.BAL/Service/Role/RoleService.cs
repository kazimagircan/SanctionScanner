using SanctionScanner.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanctionScanner.BAL.Service.Role
{
    public class RoleService : IRoleService
    {
        private UnitOfWork uow;
        public RoleService()
        {
            uow = new UnitOfWork();
        }
        public bool AddRole(RoleDto data)
        {
            bool result = false;

            try
            {
                DAL.Model.Entity.Role model = new DAL.Model.Entity.Role
                {
                    Name = data.Name,
                    Description = data.Description,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,

                };
                uow.Repository<DAL.Model.Entity.Role>().Add(model);
                uow.SaveChanges();

                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool DeleteRole(int id)
        {
            var role = uow.Repository<DAL.Model.Entity.Role>().GetById(id);
            var isExistUser = uow.Repository<DAL.Model.Entity.UserRoles>().GetBy(x => id == x.RoleId && x.IsActive == true).FirstOrDefault();

            if (role == null)
                return false;

            if (isExistUser != null)
                return false;

            uow.Repository<DAL.Model.Entity.Role>().Delete(id);
            uow.SaveChanges();
            return true;
        }

        public List<RoleDto> GetRoles()
        {
            return uow.Repository<DAL.Model.Entity.Role>()
               .All()
               .Select(x => new RoleDto()
               {
                   Id = x.Id,
                   CreatedDate = (DateTime)x.CreatedDate,
                   UpdatedDate = (DateTime)x.UpdatedDate,
                   Name = x.Name,
                   Description = x.Description

               }).ToList();
        }

        public int RoleExists(string name)
        {
            int result=0;

            try
            {
                DAL.Model.Entity.Role role = uow.Repository<DAL.Model.Entity.Role>().GetBy(x => x.Name == name).FirstOrDefault();

                if (role != null)
                    result = role.Id;
                
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool UpdateRole(RoleDto data)
        {
            bool result = false;

            try
            {
                DAL.Model.Entity.Role role = uow.Repository<DAL.Model.Entity.Role>().GetBy(x => x.Id == data.Id).FirstOrDefault();

                if (role != null)
                {
                    role.Name = data.Name;
                    role.Description = data.Description;
                    role.UpdatedDate = DateTime.UtcNow;

                    uow.Repository<DAL.Model.Entity.Role>().Edit(role);
                    uow.SaveChanges();

                    result = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
