using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace paymobd.Models
{
    public class transection_final
    {

        public int ID { get; set; }
        [Display(Name = "UserName")]
        public string username { get; set; }
        [Display(Name = "Sent To")]
        public string send_to { get; set; }
        [Display(Name = "Date")]
        public DateTime sent_date { get; set; }
        [Display(Name = "Amount")]
        public decimal ammount { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
    }
}