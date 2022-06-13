using SanctionScanner.BAL.Service.Cost;
using SanctionScanner.BAL.Service.UserRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ManagerId { get; set; }
        public string Password { get; set; }

        public ICollection<CostDto> Costs { get; set; }
        public ICollection<UserRoleDto> UserRoles { get; set; }

    }
}
