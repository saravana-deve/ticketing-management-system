using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.Models.Department
{
    public class Department
    {
        [DisplayName("Department Code")]
        [Required(ErrorMessage ="Enter the Department Code")]
        public string Dept_Code { get; set; }
        [DisplayName("Department Name")]
        [Required(ErrorMessage = "Enter the Department Name")]
        public string Dept_Name { get; set; }
        public string Btn { get; set; }
        public string Type { get; set; }
        public List<Department>List { get; set; }
    }
}