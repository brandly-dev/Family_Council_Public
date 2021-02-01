using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblUserSurvey
    {
        public long Id { get; set; }
        public long? PoolSurveyId { get; set; }
        public long? UserId { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? SentDate { get; set; }
    }
}
