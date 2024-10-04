using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Models.Ticket
{
    public class Ticket
    {
        [DisplayName("Ticket Id")]
        [Required(ErrorMessage = "Enter the Ticket Id!")]
        public string Ticket_id { get; set; }
        [Required(ErrorMessage = "Choose the Category!")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Enter the Narration!")]
        public string Narration { get; set; }
        [Required(ErrorMessage = "Choose the Priority!")]
        public string Priority { get; set; }
        public string Date { get; set; }
        public string Frm_Stn_Code { get; set; }
        public string Frm_BCode { get; set; }
        public string Dept_Name { get; set; }
        [DisplayName("Contact No")]
        [Required(ErrorMessage = "Enter the Contact No!")]
        public string Contact_No { get; set; }
        public string Category_Code { get; set; }
        [DisplayName("Assign Person")]
        [Required(ErrorMessage = "Choose the Assigned Person!")]
        public string NUser { get; set; }
        [DisplayName("Sub Category")]
        [Required(ErrorMessage = "Choose the Sub Category!")]
        public string Sub_Category { get; set; }
        public string Sub_Category_Name { get; set; }
        public string Notes { get; set; }

        public String ImageData { get; set; }

        public string Btn { get; set; }
        public string Type { get; set; }
        public List<Ticket> List { get; set; }
        public IEnumerable<SelectListItem> N_List { get; set; }
        public IEnumerable<SelectListItem> U_List { get; set; }
        public IEnumerable<SelectListItem> Sub_Cate_List { get; set; }
        public IEnumerable<SelectListItem> Priot_List { get; set; }
    }
    public class Image
    {
        public byte[] ImageData { get; set; }
        public string FileName { get; set; }
    }
}