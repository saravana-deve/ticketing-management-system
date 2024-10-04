using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.Models.Designation
{
    public class Designation
    {

        [DisplayName("Designation Code")]
        [Required(ErrorMessage = "Enter the Designation Code!")]
        public string Desi_Code { get; set; }
        [DisplayName("Designation Name")]
        [Required(ErrorMessage = "Enter the Designation Name!")]
        public string Desi_Name { get; set; }
        public string Btn { get; set; }
        public string Type { get; set; }
        public List<Designation> List { get; set; }
    }
}