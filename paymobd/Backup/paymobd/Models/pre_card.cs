using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace paymobd.Models
{
    public class pre_card
    {
        public int ID { get; set; }

        [Display(Name = "Operator:")]
        public string operator_name { get; set; }
        [Display(Name = "Amount:")]
        public double pre_ammount { get; set; }
        [Display(Name = "Card Number:")]
        public string pre_number { get; set; }
    }
}