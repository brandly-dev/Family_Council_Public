using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblForm
    {
        public TblForm()
        {
            TblSurvey = new HashSet<TblSurvey>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string GoogleFormId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? IsActive { get; set; }

        public virtual ICollection<TblSurvey> TblSurvey { get; set; }
    }
}
