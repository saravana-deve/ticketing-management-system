using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Models.User
{
    public class User
    {
        [DisplayName("User Code")]
        [Required(ErrorMessage = "Enter the User Code!")]
        public string Ucode { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "Enter the User Name!")]
        public string UName { get; set; }   
        [DisplayName("Contact No")]
        [Required(ErrorMessage = "Enter the Contact No!")]
        [RegularExpression(@"^[6-9]\d{9,}$", ErrorMessage = "must start with a digit between 6 and 9 and contain exactly 10 digits.")]
        public string Contact { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Enter the Password!")]
        public string Pword { get; set; }
        [DisplayName("Department")]
        [Required(ErrorMessage = "Choose the Department!")]
        public string Dept_Code { get; set; }
        [DisplayName("Designation")]
        [Required(ErrorMessage = "Choose the Designation!")]
        public string Desi_Code { get; set; }
        [DisplayName("Station Code")]
        [Required(ErrorMessage = "Choose the Station Code!")]
        public string Stn_Code { get; set; }
        [DisplayName("Branch Code")]
        [Required(ErrorMessage = "Choose the Branch Code!")]
        public string BCode { get; set; }    
        [DisplayName("Rights")]
        [Required(ErrorMessage = "Choose the Rights!")]
        public string Rights { get; set; }
        public string Rights_Name { get; set; }
        public string Dept_Name { get; set; }
        public string Desi_Name { get; set; }
        public string Btn { get; set; }
        public string Type { get; set; }
        public string Hide_Icon { get; set; }
        public List<User> List { get; set; }

        //DropDown List
        public IEnumerable<SelectListItem> Stn_List { get; set; }
        public IEnumerable<SelectListItem> Branch_List { get; set; }
        public IEnumerable<SelectListItem> Dep_List { get; set; }
        public IEnumerable<SelectListItem> Desi_List { get; set; }
        public IEnumerable<SelectListItem> Right_List { get; set; }
    }
}