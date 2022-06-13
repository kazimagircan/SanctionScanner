using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.UserRoles
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
