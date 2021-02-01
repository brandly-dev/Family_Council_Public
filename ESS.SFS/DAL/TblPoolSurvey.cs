using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESS.SFS.DAL
{
    public partial class TblPoolSurvey
    {
        public TblPoolSurvey()
        {
            TblUserSurvey = new HashSet<TblUserSurvey>();
        }

        public long Id { get; set; }
        public long? SurveyId { get; set; }
        public long? PoolTemplateId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual TblPool PoolTemplate { get; set; }
        public virtual TblSurvey Survey { get; set; }
        public virtual ICollection<TblUserSurvey> TblUserSurvey { get; set; }
        [NotMapped]
        public bool? IsSendToAll { get; set; }
        [NotMapped]
        public bool? IsNewTemplate { get; set; }
    }
}
