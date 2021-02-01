using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblForm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? IsActive { get; set; }
    }
}
