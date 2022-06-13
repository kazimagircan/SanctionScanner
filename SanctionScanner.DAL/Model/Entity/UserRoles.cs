using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SanctionScanner.DAL.Model.Entity
{
    [Table("USERROLES")]

    public class UserRoles : BaseEntity
    {
        public string Name { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }


        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }


    }
}
