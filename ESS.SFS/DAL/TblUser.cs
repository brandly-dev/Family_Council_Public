using System;
using System.Collections.Generic;

namespace ESS.SFS.DAL
{
    public partial class TblUser
    {
        public long Id { get; set; }
        public string JobTittle { get; set; }
        public int MaritalStatus { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public long? City { get; set; }
        public string Status { get; set; }
        public bool? IsEmailVerified { get; set; }
        public int? CurrentCreditPoints { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }

        public string ProfilePicture { get; set; }

        public virtual TblCity CityNavigation { get; set; }

        public virtual ICollection<TblPayment> TblPayment { get; set; }
    }
}
