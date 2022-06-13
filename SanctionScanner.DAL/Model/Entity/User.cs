using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SanctionScanner.DAL.Model.Entity
{
    [Table("USER")]
    public class User : BaseEntity
    {
        public User()
        {
            this.Costs = new HashSet<Cost>();
            this.UserRoles = new HashSet<UserRoles>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ManagerId { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }

    }
}
