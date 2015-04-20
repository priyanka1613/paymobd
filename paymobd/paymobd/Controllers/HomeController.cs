using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paymobd.Models;
using System.Data;
using System.Web.Security;
using System.Threading;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace paymobd.Controllers
{
    public class HomeController : Controller
    {
        PayMoDb db = new PayMoDb();

        public ActionResult ip()
        {


            //ip 
            //string ip = Request.UserHostAddress;
            string ip = "213.114.158.229";
            ViewBag.ip = ip;

            //location finding   
            //datatable column contents are:
            //host,countryName,city,region,latitude,longitude,timezone;
   
            location lc = new location();
            DataTable dt = lc.GetLocation(ip.Trim());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    ViewBag.city = dt.Rows[0]["city"].ToString();
                    ViewBag.ip = dt.Rows[0]["host"].ToString();
                    ViewBag.region = dt.Rows[0]["region"].ToString();
                    ViewBag.country = dt.Rows[0]["countryName"].ToString();
                    ViewBag.latitude = dt.Rows[0]["latitude"].ToString();
                    ViewBag.longitude = dt.Rows[0]["longitude"].ToString();

                }

                else
                {
                    ViewBag.city = "invalid";
                }

            }
            else
            {
                ViewBag.city = "invalid 2nd";
            }

            Thread.Sleep(1000);
            return View();


        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("myprofile", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Index_bangla()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("myprofile", "Home");
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "aa1613")]
        public ActionResult AllProfiles()
        {
            ViewBag.total = db.userprofiles.Count();
            var x = db.userprofiles.ToList();
            return View(x);
        }

        [Authorize(Roles = "aa1613")]
        [HttpPost]
        public ActionResult search_profile(string profile)
        {

            var p = db.userprofiles.Where(r => r.username == profile).ToList();
            return PartialView(p);

        }

        [Authorize(Roles = "aa1613")]
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
                var z = db.wallet_deposit_temp.ToList();
                if (z.Count() > 0)
                {
                    foreach (var i in z)
                    {
                        db.wallet_deposit_temp.Remove(i);
                    }
                }
                var v = db.refil_history.Where(r => r.username == tr.username).ToList();
                if (v.Count() > 0)
                {
                    var v1 = v.ToArray();
                    v1[0].ammount = tr.transection_ammount;
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
                    else
                    {
                        url += "&email=" + "youremail@server.com";
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

        public ActionResult Instant_refill_bangla()
        {
            List<decimal> li1 = new List<decimal> { 100, 200, 300, 400, 500 };
            ViewBag.ammount = li1;
            return PartialView();

        }
        [HttpPost]
        public ActionResult Instant_refill_bangla(Transection tr)
        {
            if (ModelState.IsValid)
            {
                var z = db.wallet_deposit_temp.ToList();
                if (z.Count() > 0)
                {
                    foreach (var i in z)
                    {
                        db.wallet_deposit_temp.Remove(i);
                    }
                }
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
                    else
                    {
                        url += "&email=" + "youremail@server.com";
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

        [Authorize]
        public ActionResult myprofile()
        {
            if (Request.IsAuthenticated)
            {
                string s = User.Identity.Name;
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                return View(x);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [Authorize]
        public ActionResult myprofile_bangla()
        {
            if (Request.IsAuthenticated)
            {
                string s = User.Identity.Name;
                var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                ViewBag.wallet_money = x.wallet_money;
                return View(x);
            }
            else
            {
                return RedirectToAction("Index_bangla", "Home");
            }

        }

        [Authorize]
        public ActionResult wallet_refill()
        {
            /*
            Transection tr = new Transection();
            tr.username = User.Identity.Name;
            tr.mo_number = "walletrefill";
            tr.ConfirmNumber = "walletrefill";
            tr.transection_date = DateTime.Now;*/
            List<decimal> li1 = new List<decimal> { 2000, 3000, 4000, 5000 };
            ViewBag.ammount = li1;

            return PartialView();
        }

        [HttpPost]
        public ActionResult wallet_refill(Transection tr)
        {

            if (ModelState.IsValid)
            {
                var z = db.refil_history.ToList();
                if (z.Count() > 0)
                {
                    foreach (var i1 in z)
                    {
                        db.refil_history.Remove(i1);
                    }

                }
                var v = db.wallet_deposit_temp.Where(r => r.username == tr.username).ToList();
                if (v.Count() > 0)
                {
                    var v1 = v.ToArray();
                    v1[0].deposit_ammount = tr.transection_ammount;
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

            }
            else
            {
                ModelState.AddModelError(" ", "Please provide valid data");
            }
            return PartialView(tr);
        }

        [Authorize]
        public ActionResult wallet_refill_bangla()
        {

            List<decimal> li1 = new List<decimal> { 2000, 3000, 4000, 5000 };
            ViewBag.ammount = li1;
            return PartialView();
        }

        [HttpPost]
        public ActionResult wallet_refill_bangla(Transection tr)
        {

            if (ModelState.IsValid)
            {
                var z = db.refil_history.ToList();
                if (z.Count() > 0)
                {
                    foreach (var i1 in z)
                    {
                        db.refil_history.Remove(i1);
                    }

                }
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

            if (Request.IsAuthenticated)
            {
                string s = User.Identity.Name;
                //checking deposit in wallet teporary folder
                var x = db.wallet_deposit_temp.Where(r => r.username == s).FirstOrDefault();
                if (!string.IsNullOrEmpty(x.username))
                {
                    //var x1 = x.ToArray();
                    user_deposit_history udh = new user_deposit_history();
                    udh.username = s;
                    udh.deposit_status = "Approved";
                    udh.deposit_date = x.deposit_date;
                    udh.deposit_ammount = x.deposit_ammount;
                    db.wallet_deposit_temp.Remove(x);
                    db.deposit_history.Add(udh);

                    //updating user wallet money

                    var p = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
                    p.wallet_money = p.wallet_money + x.deposit_ammount;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("myprofile", "Home");
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
                /*string s = "UnknowN";
                //updating instant refill history
                transection_final tf = new transection_final();
                //var y = db.refil_history.Where(r => r.username == s).FirstOrDefault();
                tf.username = s;
                tf.status = "Pending";
                tf.sent_date =  DateTime.Now;
                tf.send_to = "Instant recharge";
                tf.ammount = 0;

                db.transection_finals.Add(tf);
                //db.refil_history.Remove(y);
                db.SaveChanges();*/
                return RedirectToAction("myprofile", "Home");

            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public ActionResult refill_by_wallet()
        {

            string s = User.Identity.Name;
            var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
            decimal d = x.wallet_money;

            List<decimal> li1 = new List<decimal>();
            for (int i = 200; i <= d; i = i + 100)
            {
                li1.Add(i);
            }
            ViewBag.wallet_money = x.wallet_money;
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

                //send mail
                MailMessage message = new MailMessage();
                var fromAddress = "info@paymobd.com";
                var toAddress = "arif1613@yahoo.com";
                const string fromPassword = "aa1613++";
                string subject = "Wallet refill";
                string body = string.Format("{0} has sent {1} to {2}", User.Identity.Name, tr.transection_ammount, tr.mo_number);

                //SmtpClient smtp = new SmtpClient
                //{
                //    Host = "mail.paymobd.com",
                //    Port = 587,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential(fromAddress, fromPassword)
                //};
                //smtp.Send(fromAddress, toAddress, subject, body);
                //Thread.Sleep(2000);
                return RedirectToAction("order_confirm");
            }
            else
            {
                ModelState.AddModelError(" ", "Please provide valid data");
            }
            return PartialView(tr);
        }

        public ActionResult order_confirm()
        {

            string s = User.Identity.Name;
            var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
            ViewBag.wallet_money = x.wallet_money;
            return View();

        }


        [Authorize]
        public ActionResult refill_by_wallet_bangla()
        {

            string s = User.Identity.Name;
            var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
            decimal d = x.wallet_money;

            List<decimal> li1 = new List<decimal>();
            for (int i = 200; i <= d; i = i + 200)
            {
                li1.Add(i);
            }
            ViewBag.wallet_money = x.wallet_money;
            ViewBag.ammount = li1;
            return PartialView();
        }

        [HttpPost]
        public ActionResult refill_by_wallet_bangla(Transection tr)
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

                //send mail
                MailMessage message = new MailMessage();
                var fromAddress = "info@paymobd.com";
                var toAddress = "arif1613@yahoo.com";
                const string fromPassword = "aa1613++";
                string subject = "Wallet refill-bangla";
                string body = string.Format("{0} has sent {1} to {2}", User.Identity.Name, tr.transection_ammount, tr.mo_number);
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.paymobd.com",
                    Port = 587,
                    EnableSsl = false,
                    Credentials = new NetworkCredential(fromAddress, fromPassword)
                };
                smtp.Send(fromAddress, toAddress, subject, body);

                Thread.Sleep(2000);
                return RedirectToAction("order_confirm_bangla");
            }
            else
            {
                ModelState.AddModelError(" ", "Please provide valid data");
            }
            return PartialView(tr);
        }
        public ActionResult order_confirm_bangla()
        {

            string s = User.Identity.Name;
            var x = db.userprofiles.Where(r => r.username == s).FirstOrDefault();
            ViewBag.wallet_money = x.wallet_money;
            return View();

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
        [Authorize]
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
        [Authorize(Roles = "aa1613")]
        public ActionResult Delete_refill(int id)
        {
            var x = db.transection_finals.Find(id);
            db.transection_finals.Remove(x);
            db.SaveChanges();
            return RedirectToAction("mob_refill_history", "Home");
        }
        [Authorize(Roles = "aa1613")]
        public ActionResult Delete_wallet_deposit(int id)
        {
            var x = db.deposit_history.Find(id);
            db.deposit_history.Remove(x);
            db.SaveChanges();
            return RedirectToAction("Wallet_refill_history", "Home");
        }
        [Authorize(Roles = "aa1613")]
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

        [Authorize(Roles = "aa1613")]
        //temporary database
        public ActionResult temp_db()
        {
            var v = db.refil_history.ToList();
            return View(v);
        }

        [Authorize(Roles = "aa1613")]
        public ActionResult temp_delete(int id)
        {
            var x = db.refil_history.Find(id);
            db.refil_history.Remove(x);
            db.SaveChanges();
            return RedirectToAction("temp_db");
        }
        [Authorize(Roles = "aa1613")]
        //temporary wallet deposits
        public ActionResult temp_wallet()
        {
            var v = db.wallet_deposit_temp.ToList();
            return View(v);
        }

        [Authorize(Roles = "aa1613")]
        public ActionResult temp_wallet_delete(int id)
        {
            var x = db.wallet_deposit_temp.Find(id);
            db.wallet_deposit_temp.Remove(x);
            db.SaveChanges();
            return RedirectToAction("temp_wallet");
        }

        [AllowAnonymous]
        public ActionResult help_center()
        {

            return View();
        }

        [Authorize(Roles = "aa1613")]
        public ActionResult message()
        {
            return View();
        }
        [HttpPost]
        public ActionResult message(string sub, string mes)
        {
            var v = db.userprofiles.Select(r => r.e_mail).ToList();
            foreach (var i in v)
            {
                //send mail
                MailMessage message = new MailMessage();
                var fromAddress = "info@paymobd.com";
                // any address where the email will be sending
                var toAddress = i;
                //Password of your gmail address
                const string fromPassword = "aa1613++";
                // Passing the values and make a email formate to display
                string subject = sub;
                string body = mes;
                // smtp settings
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.paymobd.com",
                    Port = 587,
                    EnableSsl = false,
                    //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    Credentials = new NetworkCredential(fromAddress, fromPassword)
                };
                try
                {
                    smtp.Send(fromAddress, toAddress, subject, body);
                }
                catch
                {
                }
            }
            return RedirectToAction("message");
        }

        public ActionResult contact()
        {
            return View();
        }

    }
}

