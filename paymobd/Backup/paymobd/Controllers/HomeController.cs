using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paymobd.Models;
using System.Data;
using System.Web.Security;

namespace paymobd.Controllers
{
    public class HomeController : Controller
    {
        PayMoDb db = new PayMoDb();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("myprofile","Home");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult AllProfiles()
        {
            var x = db.userprofiles.ToList();
            return View(x);
        }
        //edit user profile
        public ActionResult Edit_profile(int id)
        {
            var x = db.userprofiles.Find(id);
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit_profile(UserProfile up)
        {
            db.Entry(up).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllProfiles", "Home");
        }
        public ActionResult Instant_refill()
        {
            List<decimal> li1 = new List<decimal> { 100, 200, 300, 400, 500 };
            ViewBag.ammount = li1;
            return PartialView();

        }
        [HttpPost]
        public ActionResult Instant_refill(Transection tr)
        {
            if (ModelState.IsValid)
            {
                var v = db.refil_history.Where(r => r.username == tr.username).ToList();
                if (v.Count() > 0)
                {
                    var v1 = v.ToArray();
                    //User_refill_history rh = new User_refill_history();
                    v1[0].ammount = tr.transection_ammount;
                    //rh.ammount = tr.transection_ammount;
                    db.Entry(v1[0]).State = EntityState.Modified;
                    db.SaveChanges();
                    string url = paypal.paypal_pay(tr);
                    if (Request.IsAuthenticated)
                    {
                        string email = db.userprofiles.
                            Where(r => r.username == User.Identity.Name).Select(r => r.e_mail).FirstOrDefault();
                        url += "&email=" + email;
                        Response.Redirect(url);
                    }
                    else {
                        url += "&email=" +"youremail@server.com";
                        Response.Redirect(url);
                    }
                    
                }
                else
                {

                    User_refill_history rh = new User_refill_history();
                    rh.username = tr.username;
                    rh.send_to = tr.mo_number;
                    rh.sent_date = tr.transection_date;
                    rh.status = "Pending";
                    rh.ammount = tr.transection_ammount;
                    db.refil_history.Add(rh);
                    //db.transections.Add(tr);
                    db.SaveChanges();
                    string url = paypal.paypal_pay(tr);
                    Response.Redirect(url);
                    //return RedirectToAction("Thank_You");
                }
                //if (Request.IsAuthenticated)
                //{
                //  return RedirectToAction("myprofile");
                //}
                //else
                //{
                //  return RedirectToAction("Index");
                //}
                //}
            }
            else
            {
                ModelState.AddModelError("", "Please provide valid data.");
            }
            return PartialView(tr);
        }

        public ActionResult myprofile()
        {
            if (Request.IsAuthenticated)
            {
                string s = User.Identity.Name;
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                return View(x);
            }
            else {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult wallet_refill()
        {

            List<decimal> li1 = new List<decimal> { 2000, 3000, 4000, 5000 };
            ViewBag.ammount = li1;
            Transection tr = new Transection();
            tr.username = User.Identity.Name;
            tr.mo_number = "walletrefil";
            tr.ConfirmNumber = "walletrefil";
            tr.transection_date = DateTime.Now;
            return PartialView(tr);
        }

        [HttpPost]
        public ActionResult wallet_refill(Transection tr)
        {

            if (ModelState.IsValid)
            {
                var v = db.wallet_deposit_temp.Where(r => r.username == tr.username).ToList();
                if (v.Count() > 0)
                {
                    var v1 = v.ToArray();
                    //wallet_deposit_temp wdt = new wallet_deposit_temp();
                    v1[0].deposit_ammount = tr.transection_ammount;
                    //wdt.deposit_ammount = tr.transection_ammount;
                    db.Entry(v1[0]).State = EntityState.Modified;
                    db.SaveChanges();
                    string url = paypal_wallet.paypal_wallet_pay(tr);
                    string email = db.userprofiles.
                        Where(r => r.username == User.Identity.Name).Select(r => r.e_mail).FirstOrDefault();
                    url += "&email=" + email;
                    Response.Redirect(url);

                    //return RedirectToAction("Thank_You");

                }
                else
                {
                    wallet_deposit_temp wdt = new wallet_deposit_temp();
                    wdt.username = tr.username;
                    wdt.deposit_date = tr.transection_date;
                    wdt.deposit_ammount = tr.transection_ammount;
                    db.wallet_deposit_temp.Add(wdt);
                    db.SaveChanges();
                    string url = paypal_wallet.paypal_wallet_pay(tr);
                    string email = db.userprofiles.
                        Where(r => r.username == User.Identity.Name).Select(r => r.e_mail).FirstOrDefault();
                    url += "&email=" + email;
                    Response.Redirect(url);

                    //return RedirectToAction("Thank_You");
                }

                /*user_deposit_history udh = new user_deposit_history();
                udh.username = tr.username;
                udh.deposit_date = tr.transection_date;
                udh.deposit_ammount = tr.transection_ammount;
                udh.deposit_status = "Approved";
                db.deposit_history.Add(udh);

                var d = db.userprofiles
                    .Where(r => r.username == tr.username).FirstOrDefault();
                d.wallet_money = d.wallet_money + udh.deposit_ammount;
                db.Entry(d).State = EntityState.Modified;*/
                //db.SaveChanges();
                //string url = paypal_wallet.paypal_wallet_pay(tr);
                //Response.Redirect(url);
                //return RedirectToAction("Thank_You");
            }
            else
            {
                ModelState.AddModelError(" ", "Please provide valid data");
            }
            return PartialView(tr);
        }
        public ActionResult Thank_You()
        {
            if (Request.IsAuthenticated)
            {
                string s = User.Identity.Name;
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Thank_You_post()
        {
            string s;
            if (Request.IsAuthenticated)
            {
                s = User.Identity.Name;
            }
            else
            {
                s = "UnknowN";
            }

            if (Request.IsAuthenticated)
            {


                //checking deposit in wallet teporary folder
                var x = db.wallet_deposit_temp.Where(r => r.username == s).ToList();
                if (x.Count() > 0)
                {
                    var x1 = x.ToArray();
                    user_deposit_history udh = new user_deposit_history();
                    udh.username = s;
                    udh.deposit_status = "Approved";
                    udh.deposit_date = x1[0].deposit_date;
                    udh.deposit_ammount = x1[0].deposit_ammount;
                    db.deposit_history.Add(udh);
                    db.wallet_deposit_temp.Remove(x1[0]);

                    //updating user wallet money

                    var p = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                    p.wallet_money = p.wallet_money + x1[0].deposit_ammount;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();


                    return RedirectToAction("Index", "Home");
                }

                //updating instant refill history
                transection_final tf = new transection_final();
                var y = db.refil_history.Where(r => r.username == s).FirstOrDefault();
                tf.username = y.username;
                tf.status = y.status;
                tf.sent_date = y.sent_date;
                tf.send_to = y.send_to;
                tf.ammount = y.ammount;
                db.transection_finals.Add(tf);
                db.refil_history.Remove(y);
                db.SaveChanges();
            }
            else
            {

                //updating instant refill history
                transection_final tf = new transection_final();
                var y = db.refil_history.Where(r => r.username == s).FirstOrDefault();
                tf.username = y.username;
                tf.status = y.status;
                tf.sent_date = y.sent_date;
                tf.send_to = y.send_to;
                tf.ammount = y.ammount;
                db.transection_finals.Add(tf);
                db.refil_history.Remove(y);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult refill_by_wallet()
        {

            string s = User.Identity.Name;
            var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
            decimal d = x.wallet_money;

            List<decimal> li1 = new List<decimal>();
            for (int i = 100; i <= d; i = i + 100)
            {
                li1.Add(i);
            }

            ViewBag.ammount = li1;
            return PartialView();
        }

        [HttpPost]
        public ActionResult refill_by_wallet(Transection tr)
        {
            if (ModelState.IsValid)
            {
                //updating wallet money info
                var x = db.userprofiles.Where(r => r.username == tr.username).FirstOrDefault();
                x.wallet_money = x.wallet_money - tr.transection_ammount;
                db.Entry(x).State = EntityState.Modified;
                db.SaveChanges();

                //updating refill history
                transection_final tf = new transection_final();
                //User_refill_history r_h = new User_refill_history();
                tf.ammount = tr.transection_ammount;
                tf.send_to = tr.mo_number;
                tf.sent_date = tr.transection_date;
                tf.status = "Pending";
                tf.username = tr.username;
                db.transection_finals.Add(tf);
                db.SaveChanges();
                return RedirectToAction("myprofile");
            }
            else
            {
                ModelState.AddModelError(" ", "Please provide valid data");
            }
            return PartialView(tr);
        }

        public ActionResult delete_user(string u_name)
        {

            var user_profile = db.userprofiles.Where(r => r.username == u_name).FirstOrDefault();
            db.userprofiles.Remove(user_profile);
            Membership.DeleteUser(u_name);

            var user_deposit = db.deposit_history.Where(r => r.username == u_name).ToList();
            if (user_deposit.Count() > 0)
            {
                foreach (var d in user_deposit)
                {
                    db.deposit_history.Remove(d);
                }
            }
            var user_refill = db.refil_history.Where(r => r.username == u_name).ToList();
            if (user_refill.Count() > 0)
            {
                foreach (var re in user_refill)
                {
                    db.refil_history.Remove(re);

                }
            }
            db.SaveChanges();
            return RedirectToAction("AllProfiles", "Home");
        }
        [Authorize]
        public ActionResult Wallet_refill_history()
        {
            string s = User.Identity.Name;
            if (User.IsInRole("aa1613"))
            {
                var v = db.deposit_history.OrderByDescending(r => r.deposit_date).ToList();
                //string s = User.Identity.Name;
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                return View(v);
            }
            else
            {
                var v = db.deposit_history.OrderByDescending(r => r.deposit_date).Where(r => r.username == s).ToList();
                //string s = User.Identity.Name;
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                return View(v);
            }
        }

        public ActionResult mob_refill_history()
        {
            string s = User.Identity.Name;
            //filter list
            List<string> li = new List<string> { "Pending", "Sent", "Declined", "Prepaid card" };
            ViewBag.filtering = li;
            if (User.IsInRole("aa1613"))
            {
                var v = db.transection_finals.OrderByDescending(r => r.sent_date).ToList();
                var v1 = v.OrderBy(r => r.send_to).ToList();
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;

                //total money
                decimal m = 0;
                foreach (var i in v1)
                {
                    m = m + i.ammount;
                }
                ViewBag.total = m;
                return View(v1);
            }
            else
            {
                var v = db.transection_finals.OrderByDescending(r => r.sent_date).Where(r => r.username == s).ToList();
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                return View(v);
            }
        }

        [HttpPost]
        public ActionResult Filter(string filterby)
        {
            string s = User.Identity.Name;
            //filter list
            List<string> li = new List<string> { "Pending", "Sent", "Declined", "Prepaid card" };
            ViewBag.filtering = li;
            if (User.IsInRole("aa1613"))
            {
                var v = db.transection_finals.OrderByDescending(r => r.sent_date).ToList();
                var v1 = v.Where(r => r.status == filterby).ToList();
                var v2 = v1.OrderBy(r => r.send_to).ToList();
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                //total money
                var z = db.transection_finals.Where(r => r.status == filterby).ToList();
                decimal m = 0;
                foreach (var i in z)
                {
                    m = m + i.ammount;
                }
                ViewBag.total = m;

                return View("mob_refill_history", v2);
            }
            else
            {
                var v = db.refil_history.OrderByDescending(r => r.sent_date).Where(r => r.username == s).ToList();
                var v1 = v.Where(r => r.status == filterby).ToList();
                var v2 = v1.OrderBy(r => r.send_to).ToList();
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                return View("mob_refill_history", v2);
            }
            //return View("mob_refill_history", v);
        }
        public ActionResult Delete_refill(int id)
        {
            var x = db.transection_finals.Find(id);
            db.transection_finals.Remove(x);
            db.SaveChanges();
            return RedirectToAction("mob_refill_history", "Home");
        }

        public ActionResult Delete_wallet_deposit(int id)
        {
            var x = db.deposit_history.Find(id);
            db.deposit_history.Remove(x);
            db.SaveChanges();
            return RedirectToAction("Wallet_refill_history", "Home");
        }

        [HttpPost]
        public ActionResult change_status(int id, string status)
        {
            var x = db.transection_finals.Find(id);

            if (status == "Sent")
            {
                x.status = "Sent";
                db.Entry(x).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("mob_refill_history", "Home");
            }
            else if (status == "Declined")
            {
                x.status = "Declined";
                db.Entry(x).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("mob_refill_history", "Home");
            }
            else
            {
                return RedirectToAction("mob_refill_history", "Home");
            }
        }

        //temporary database
        public ActionResult temp_db()
        {
            var v = db.refil_history.ToList();
            return View(v);
        }

        public ActionResult temp_delete(int id)
        {
            var x = db.refil_history.Find(id);
            db.refil_history.Remove(x);
            db.SaveChanges();
            return RedirectToAction("temp_db");
        }
        //temporary wallet deposits
        public ActionResult temp_wallet()
        {
            var v = db.wallet_deposit_temp.ToList();
            return View(v);
        }
        public ActionResult temp_wallet_delete(int id)
        {
            var x = db.wallet_deposit_temp.Find(id);
            db.wallet_deposit_temp.Remove(x);
            db.SaveChanges();
            return RedirectToAction("temp_wallet");
        }
    }
}

