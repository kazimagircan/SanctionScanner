using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.Role
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
