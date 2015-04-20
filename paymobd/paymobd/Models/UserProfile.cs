using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace paymobd.Models
{
    public class UserProfile
    {
        public int ID { get; set; }
        [Display(Name = "UserName")]
        public string username { get; set; }
        [Display(Name = "E-mail")]
        public string e_mail { get; set; }
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Joining Date")]
        public DateTime join_date { get; set; }
        [Display(Name = "Wallet Money")]
        public decimal wallet_money { get; set; }
    }
}