using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblCity
    {
        public TblCity()
        {
            TblUser = new HashSet<TblUser>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? CountryId { get; set; }

        public virtual ICollection<TblUser> TblUser { get; set; }
    }
}
