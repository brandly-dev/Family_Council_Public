using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblSurveyPoolCity
    {
        public long Id { get; set; }
        public long? SurveyPoolId { get; set; }
        public int? CityId { get; set; }
    }
}
