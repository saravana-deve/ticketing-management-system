using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.Models.Rights
{
    public class Rights
    {
        [DisplayName("Rights Id")]
        [Required(ErrorMessage = "Enter the Rights Id!")]
        public string Rights_Id { get; set; }
        [DisplayName("Rights Name")]
        [Required(ErrorMessage = "Enter the Rights Name!")]
        public string Rights_Name { get; set; }
        public string Btn { get; set; }
        public string Type { get; set; }
        public List<Rights> List { get; set; }
    }
}