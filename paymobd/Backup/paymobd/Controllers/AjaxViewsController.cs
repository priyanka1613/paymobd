using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using paymobd.Models;

namespace paymobd.Controllers
{
    public class AjaxViewsController : Controller
    {
        //
        // GET: /AjaxViews/
        PayMoDb db = new PayMoDb();
        public ActionResult Instant_Refill_Rates()
        {
            Thread.Sleep(1500);
            return PartialView();
        }
        public ActionResult Refill_by_wallet_rates() 
        {
            Thread.Sleep(1500);
            return PartialView();
        }
        public ActionResult wallet_refill_rates()
        {
            Thread.Sleep(1500);
            return PartialView();
        }
        public ActionResult price_comparison() {
            
            return View();
        }
        public ActionResult money_back() {
            
            return View();
        }

    }
}
