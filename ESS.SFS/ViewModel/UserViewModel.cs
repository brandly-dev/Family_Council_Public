using ESS.SFS.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static ESS.SFS.Helper.Common;

namespace ESS.SFS.ViewModel
{
    public class UserViewModel
    {
        public Int64? Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string email { get; set; }


        [Required(ErrorMessage = "Required")]
        [Display(Name = "Message")]
        public string message { get; set; }


        public string userName { get; set; }

        public string password { get; set; }

        public string repeatPassword { get; set; }

        public string updatePassword { get; set; }

        public string maritalStatus { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Name & Surname")]
        public string surname { get; set; }

        public string gender { get; set; }

        [Required(ErrorMessage = "Required")]
        public Int32? age { get; set; }

        [Required(ErrorMessage = "Required")]
        public string city { get; set; }

        public string country { get; set; }

        public string profilePic { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

        public List<TblGender> lstGender { get; set; }

        public List<TblFamilyStatus> lstFamilyStatus { get; set; }
        public List<DropdownListForText> lstCountry { get; set; }

        public List<TblCity> lstCity { get; set; }

        public List<TblUser> lstUserModel { get; set; }


    }
}
