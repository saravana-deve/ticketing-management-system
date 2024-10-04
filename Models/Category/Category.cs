using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Models.Category
{
    public class Category
    {
        [DisplayName("Category Code")]
        [Required(ErrorMessage = "Enter the Category Code!")]
        public string Category_Code { get; set; }
        [DisplayName("Category")]
        [Required(ErrorMessage = "Enter the Category!")]
        public string Category_Name { get; set; }
        [DisplayName("Department")]
        [Required(ErrorMessage = "Choose the Department!")]
        public string MDept_Code { get; set; }
        public string MDept_Name { get; set; }
        public string Btn { get; set; }
        public string Type { get; set; }
        public IEnumerable<SelectListItem> Dep_List { get; set; }
        public List<Category> List { get; set; }
    }
}