using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.Cost
{

    public class CostDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
        public int Status { get; set; }
        public string RejectReason { get; set; }
        public int UserId { get; set; }

    }
}
