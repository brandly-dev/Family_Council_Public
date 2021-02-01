using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESS.SFS.DAL
{
    public partial class TblPool
    {
        public long Id { get; set; }
        public string PoolName { get; set; }
        public int? Participant { get; set; }
        public int? AgeMin { get; set; }
        public int? AgeMax { get; set; }
        public string Gender { get; set; }
        public string CityIds { get; set; }
        public string FamilyStatus { get; set; }
        [NotMapped]
        public string AgeRange { get; set; }
    }
}
