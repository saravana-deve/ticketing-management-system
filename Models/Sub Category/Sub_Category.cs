using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Models.Sub_Category
{
    public class Sub_Category
    {
        [DisplayName("Sub Category Code")]
        [Required(ErrorMessage = "Enter the Sub Category Code!")]
        public string Sub_Cate_Code { get; set; }
        [DisplayName("Sub Category")]
        [Required(ErrorMessage = "Enter the Sub Category!")]
        public string Sub_Cate_Name { get; set; }
        [DisplayName("Category")]
        [Required(ErrorMessage = "Choose the Category!")]
        public string Category { get; set; }
        [DisplayName("Approval")]
        [Required(ErrorMessage = "Choose the Approval!")]
        public string R_Approval { get; set; }
        public string Category_Name { get; set; }
        public string Btn { get; set; }
        public string Type { get; set; }
        public IEnumerable<SelectListItem> Cat_List { get; set; }
        public IEnumerable<SelectListItem> R_App_List { get; set; }
        public List<Sub_Category> List { get; set; }
    }
}