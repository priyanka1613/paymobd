using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace paymobd.Models
{
    public class wallet_deposit_temp
    {
        public int ID { get; set; }
        [Display(Name = "Username:")]
        public string username { get; set; }
        [Display(Name = "Deposite Date:")]
        public DateTime deposit_date { get; set; }
        [Display(Name = "Deposit Amount:")]
        public decimal deposit_ammount { get; set; }

    }
}