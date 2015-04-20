using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace paymobd.Models
{
    public class PayMoDb : DbContext
    {
        public DbSet<UserProfile> userprofiles { get; set; }
        public DbSet<User_refill_history> refil_history { get; set; }
        public DbSet<user_deposit_history> deposit_history { get; set; }
        public DbSet<Transection> transections { get; set; }
        public DbSet<pre_card> pre_cards { get; set; }
        public DbSet<transection_final> transection_finals { get; set; }
        public DbSet<wallet_deposit_temp> wallet_deposit_temp { get; set; }
    }
}