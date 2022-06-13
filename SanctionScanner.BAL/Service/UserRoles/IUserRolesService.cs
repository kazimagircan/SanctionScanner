using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.UserRoles
{
    public interface IUserRolesService
    {
        bool AddUserRole(UserRoleDto data);
        bool UpdateUserRole(UserRoleDto data);
        bool DeleteUserRole(int id);
        bool UserRoleExists(int userId, int roleId);
        List<UserRoleDto> GetUsersRoles();
        List<string> GetUserRoles(int id);
    }
}
