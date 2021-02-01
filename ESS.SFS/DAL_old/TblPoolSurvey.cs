using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblPoolSurvey
    {
        public long Id { get; set; }
        public long? SurveyId { get; set; }
        public long? PoolTemplateId { get; set; }
    }
}
