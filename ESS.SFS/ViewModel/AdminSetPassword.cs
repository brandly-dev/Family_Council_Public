using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESS.SFS.ViewModel
{
    public class AdminSetPassword
    {
        public string id { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display]
        [Compare("password")]
        public string confirmPassword { get; set; }
    }
}
