using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblPayment
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public int? RequestedPoints { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Status { get; set; }

        public virtual TblUser User { get; set; }
    }
}
