using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblLanguages
    {
        public long Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
