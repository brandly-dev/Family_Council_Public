using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblDemographic
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
