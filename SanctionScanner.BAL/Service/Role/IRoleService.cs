using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.Role
{
    public interface IRoleService
    {
        bool AddRole(RoleDto data);
        bool UpdateRole(RoleDto data);
        bool DeleteRole(int id);
        int RoleExists(string name);
        List<RoleDto> GetRoles();
    }
}
