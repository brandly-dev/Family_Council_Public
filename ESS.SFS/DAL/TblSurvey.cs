using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblSurvey
    {
        public TblSurvey()
        {
            TblPoolSurvey = new HashSet<TblPoolSurvey>();
            TblUserSurvey = new HashSet<TblUserSurvey>();
        }

        public long Id { get; set; }
        public long? FormId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public int? Points { get; set; }
        public int? Participants { get; set; }
        public int? SurveyAnswer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TblForm Form { get; set; }
        public virtual ICollection<TblPoolSurvey> TblPoolSurvey { get; set; }
        public virtual ICollection<TblUserSurvey> TblUserSurvey { get; set; }
    }
}
