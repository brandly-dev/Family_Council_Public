using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblPayment
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int? DebitPoints { get; set; }
        public bool? IsApproved { get; set; }
        public long? UserSurveyId { get; set; }
        public string Status { get; set; }
    }
}
