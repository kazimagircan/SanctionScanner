using SanctionScanner.BAL.Service.UserRoles;
using SanctionScanner.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanctionScanner.BAL.Service.User
{
    public class UserService : IUserService
    {
        private UnitOfWork uow;
        public UserService()
        {
            uow = new UnitOfWork();
        }
        public bool AddUser(UserDto data)
        {
            bool result = false;

            DAL.Model.Entity.User foundUser = uow.Repository<DAL.Model.Entity.User>().GetBy(x => x.Id == data.Id).FirstOrDefault();
            if (foundUser == null)
            {
                DAL.Model.Entity.User model = new DAL.Model.Entity.User
                {
                    Name = data.Name,
                    CreatedDate = DateTime.UtcNow,
                    Email = data.Email,
                    ManagerId = data.ManagerId,
                    Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                    IsActive = true,
                };
                uow.Repository<DAL.Model.Entity.User>().Add(model);
                uow.SaveChanges();

                result = true;
            }
            return result;
        }

        public bool DeleteUser(int userId)
        {
            var user = uow.Repository<DAL.Model.Entity.User>().GetById(userId);

            if (user == null)
                return false;

            uow.Repository<DAL.Model.Entity.User>().Delete(userId);
            uow.SaveChanges();
            return true;
        }

        public UserDto CheckUser(string mail, string password)
        {

            var user = uow.Repository<DAL.Model.Entity.User>().GetBy(x => x.Email == mail && x.IsActive == true).Select(x => new UserDto
            {
                Id = x.Id,
                CreatedDate = (DateTime)x.CreatedDate,
                UpdatedDate = (DateTime)x.UpdatedDate,
                Name = x.Name,
                ManagerId = (int)x.ManagerId,
                Password = x.Password,
                UserRoles = x.UserRoles.Select(r => new UserRoleDto
                {
                    RoleId = r.RoleId,
                    Name = r.Name

                }).ToList(),

            }).FirstOrDefault();

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    return user;

            }

            return null;
        }

        public List<UserDto> GetUsers()
        {
            return uow.Repository<DAL.Model.Entity.User>()
               .All()
               .Select(x => new UserDto()
               {
                   Id = x.Id,
                   CreatedDate = (DateTime)x.CreatedDate,
                   UpdatedDate = (DateTime)x.UpdatedDate,
                   Name = x.Name,
                   Email = x.Email,
                   ManagerId = (int)x.ManagerId
               }).ToList();
        }

        public bool UpdateUser(UserDto data)
        {
            bool result = false;

            try
            {
                DAL.Model.Entity.User user = uow.Repository<DAL.Model.Entity.User>().GetBy(x => x.Id == data.Id).FirstOrDefault();

                if (user != null)
                {

                    user.ManagerId = data.ManagerId;
                    user.UpdatedDate = DateTime.UtcNow;

                    uow.Repository<DAL.Model.Entity.User>().Edit(user);
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

        public UserDto GetUserByMail(string mail)
        {
            return uow.Repository<DAL.Model.Entity.User>().GetBy(x => x.Email == mail && x.IsActive == true).Select(x => new UserDto
            {
                Id = x.Id,
                CreatedDate = (DateTime)x.CreatedDate,
                UpdatedDate = (DateTime)x.UpdatedDate,
                Name = x.Name,
                ManagerId = (int)x.ManagerId

            }).FirstOrDefault();
        }

        public UserDto GetUserById(int id)
        {
            return uow.Repository<DAL.Model.Entity.User>().GetBy(x => x.Id == id && x.IsActive == true).Select(x => new UserDto
            {
                Id = x.Id,
                CreatedDate = (DateTime)x.CreatedDate,
                UpdatedDate = (DateTime)x.UpdatedDate,
                Name = x.Name,
                ManagerId = (int)x.ManagerId,
                Email=x.Email
                
            }).FirstOrDefault();
        }
    }
}
