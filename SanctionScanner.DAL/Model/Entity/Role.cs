using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SanctionScanner.DAL.Model.Entity
{
    [Table("ROLES")]
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
