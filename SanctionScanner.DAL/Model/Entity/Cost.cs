using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SanctionScanner.DAL.Model.Entity
{
    [Table("COST")]
    public class Cost : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
        public int Status { get; set; }
        public string RejectReason { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
