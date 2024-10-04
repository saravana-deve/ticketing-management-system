using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.Models.Home
{
    public class Home
    {
        //Common
        public string Type { get; set; }

        //Dashboard Variables
        //public string Read { get; set; }
        //public string Un_Read { get; set; }
        //public string Ass_Other { get; set; }
        //public string Closed { get; set; }
        public string Ticket_Id { get; set; }
        public string Assign_To { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public string NullProperty { get; set; }


        //Password variables
        [DisplayName("Confirm New Password")]
        [Required(ErrorMessage = "Enter the Confirm Password!")]
        public string CNF_Pass { get; set; }
        [DisplayName("Old Password")]
        [Required(ErrorMessage = "Enter the Old Password!")]
        public string Old_Pass { get; set; }
        [DisplayName("New Password")]
        [Required(ErrorMessage = "Enter the New Password!")]
        public string New_Pass { get; set; }

    }
}