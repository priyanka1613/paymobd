using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace paymobd.Models
{
    public class Transection
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [Required]
        [Display(Name = "Number:")]
        [StringLength(11)]
        public string mo_number { get; set; }

        [Required]
        [Display(Name = "Confirm Number:")]
        [Compare("mo_number", ErrorMessage = "Numbers don't match.")]
        public string ConfirmNumber { get; set; }

        [Required]
        [Display(Name = "Amount:")]
        public decimal transection_ammount { get; set; }

        [Display(Name = "Date:")]
        public DateTime transection_date { get; set; }
        
    }
}