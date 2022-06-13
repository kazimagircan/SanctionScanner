using SanctionScanner.BAL.Service.Role;
using SanctionScanner.BAL.Service.User;
using SanctionScanner.BAL.Service.UserRoles;
using SanctionScanner.DAL.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Initalizer
{
    public class IdentityDataInitializer
    {
        public static void SeedData(IUserService userService, IRoleService roleService, IUserRolesService userRolesService)
        {
            SeedRoles(roleService);
            SeedUsers(userService);
            SeedUserRoles(userService, roleService, userRolesService);
            SeedUserManager(userService);
        }
        public static void SeedRoles(IRoleService roleService)
        {
            if (roleService.RoleExists("User")==0)
            {
                roleService.AddRole(new RoleDto { Name = "User", Description = "User" });
            }

            if (roleService.RoleExists("Manager") == 0)
            {
                roleService.AddRole(new RoleDto { Name = "Manager", Description = "Manager" });
            }


            if (roleService.RoleExists("Accountant") == 0)
            {
                roleService.AddRole(new RoleDto { Name = "Accountant", Description = "Accountant" });

            }

        }
        public static void SeedUsers(IUserService userService)
        {
            if (userService.GetUserByMail("user@gmail.com") == null)
            {

                UserDto user = new UserDto
                {
                    Name = "Dummy User",
                    Email = "user@gmail.com",
                    Password = "123",
                    
                };

                userService.AddUser(user);

            }

            if (userService.GetUserByMail("manager@gmail.com") == null)
            {

                UserDto user = new UserDto
                {
                    Name = "Dummy Manager",
                    Email = "manager@gmail.com",
                    Password = "123",

                };

                userService.AddUser(user);

            }
            if (userService.GetUserByMail("accountant@gmail.com") == null)
            {

                UserDto user = new UserDto
                {
                    Name = "Dummy Accountant",
                    Email = "accountant@gmail.com",
                    Password = "123",

                };

                userService.AddUser(user);

            }
        }
        public static void SeedUserRoles(IUserService userService, IRoleService roleService, IUserRolesService userRolesService)
        {
            var user = userService.GetUserByMail("user@gmail.com");
            if (user != null)
            {
                int roleId = roleService.RoleExists("User");
                var userRoles = userRolesService.GetUserRoles(user.Id) as List<string>;
                if (!userRoles.Contains("User"))
                {
                    UserRoleDto userRole = new UserRoleDto
                    {
                        RoleId = roleId,
                        UserId = user.Id,
                        Name= "User"
                    };

                    userRolesService.AddUserRole(userRole);

                }
                
            }

            var manager = userService.GetUserByMail("manager@gmail.com");
            if (manager != null)
            {
                int roleId = roleService.RoleExists("Manager");
                var managerRoles = userRolesService.GetUserRoles(manager.Id) as List<string>;
                if (!managerRoles.Contains("Manager"))
                {
                    UserRoleDto managerRole = new UserRoleDto
                    {
                        RoleId = roleId,
                        UserId = manager.Id,
                        Name = "Manager"
                    };

                    userRolesService.AddUserRole(managerRole);

                }
                
            }

            var accountant = userService.GetUserByMail("accountant@gmail.com");
            if (accountant != null)
            {
                int roleId = roleService.RoleExists("Accountant");
                var accountantRoles = userRolesService.GetUserRoles(accountant.Id) as List<string>;
                if (!accountantRoles.Contains("Accountant"))
                {
                    UserRoleDto accountantRole = new UserRoleDto
                    {
                        RoleId = roleId,
                        UserId = accountant.Id,
                        Name = "Accountant"
                    };

                    userRolesService.AddUserRole(accountantRole);

                }
               
            }
           
        }
        public static void SeedUserManager(IUserService userService)
        {
            var user = userService.GetUserByMail("user@gmail.com");
            var manager = userService.GetUserByMail("manager@gmail.com");

            if (user != null && manager != null)
            {
                UserDto userDto = new UserDto
                {
                    ManagerId = manager.Id,
                    Id = user.Id
                     
                };

                userService.UpdateUser(userDto);

            }


        }

    }
}
