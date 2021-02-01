using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblSurvey
    {
        public long Id { get; set; }
        public long? FormId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public int? Participants { get; set; }
    }
}
