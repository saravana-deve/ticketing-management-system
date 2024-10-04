using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.Models.Login
{
    public class Login
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage ="Enter the User Name!")]
        public string UCode { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter the Password!")]
        public string Pword { get; set; }
        //public string UCode { get; set; }
    }
}