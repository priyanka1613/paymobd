using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using paymobd.Models;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Data.Entity;

namespace paymobd.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        PayMoDb db = new PayMoDb();
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return PartialView();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("myprofile", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return RedirectToAction("Index", "Home", model);
                }
            }

            // If we got this far, something failed, redisplay form
            return PartialView(model);
        }

        [AllowAnonymous]
        public ActionResult Login_bangla()
        {
            return PartialView();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login_bangla(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("myprofile_bangla", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return RedirectToAction("Index_bangla", "Home", model);
                }
            }

            // If we got this far, something failed, redisplay form
            return PartialView(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            string s = User.Identity.Name;
            var v = db.wallet_deposit_temp.Where(r => r.username == s).ToList();
            if (v.Count() > 0)
            {
                foreach (var i in v)
                {
                    db.wallet_deposit_temp.Remove(i);
                }
            }
            var v1 = db.refil_history.Where(r => r.username == s).ToList();
            if (v1.Count() > 0)
            {
                foreach (var i1 in v1)
                {
                    db.refil_history.Remove(i1);
                }

            }
            db.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return PartialView();
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                               
                    UserProfile up = new UserProfile();
                    up.username = model.UserName;
                    up.password = model.Password;
                    up.e_mail = model.Email;
                    up.join_date = DateTime.Now;
                    up.wallet_money = 0;
                    db.userprofiles.Add(up);
                    //db.SaveChanges();
                

                if (createStatus == MembershipCreateStatus.Success)
                {
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: true);

                    //send mail
                    MailMessage message = new MailMessage();
                    var fromAddress = "info@paymobd.com";
                    // any address where the email will be sending
                    var toAddress = model.Email;
                    //Password of your gmail address
                    const string fromPassword = "aa1613++";
                    // Passing the values and make a email formate to display
                    string subject = "Welcome to PayMoBD";
                    string body = string.Format("Hello {0}. Welcome to PayMoBD.com. Your PayMoBD.com login informations are Username:{1} and Password:{2}. Please contatct info@paymobd.com if you forget your password.",
                        model.UserName, model.UserName, model.Password);

                    // smtp settings
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "mail.paymobd.com",
                        Port = 587,
                        EnableSsl = false,
                        //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        Credentials = new NetworkCredential(fromAddress, fromPassword)
                        //  smtp.Timeout = 20000;

                    };
                    try
                    {
                        smtp.Send(fromAddress, toAddress, subject, body);
                    }
                    catch
                    {
                        return RedirectToAction("Register_error");
                    }
                    return RedirectToAction("myprofile", "Home");
                }
                else
                {
                    ModelState.AddModelError(" ", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Register_bangla()
        {
            return PartialView();
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register_bangla(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                UserProfile up = new UserProfile();
                up.username = model.UserName;
                up.password = model.Password;
                up.e_mail = model.Email;
                up.join_date = DateTime.Now;
                up.wallet_money = 0;
                db.userprofiles.Add(up);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: true);

                    //send mail
                    MailMessage message = new MailMessage();
                    var fromAddress = "info@paymobd.com";
                    // any address where the email will be sending
                    var toAddress = model.Email;
                    //Password of your gmail address
                    const string fromPassword = "info1613";
                    // Passing the values and make a email formate to display
                    string subject = "Welcome to PayMoBD";
                    string body = string.Format("Hello {0}. Welcome to PayMoBD.com. Your PayMoBD.com login informations are Username:{1} and Password:{2}. PLease contatct info@paymobd.com if you forget your password.",
                        model.UserName, model.UserName, model.Password);

                    // smtp settings
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "mail.paymobd.com",
                        Port = 587,
                        EnableSsl = false,
                        //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        Credentials = new NetworkCredential(fromAddress, fromPassword)
                        //  smtp.Timeout = 20000;

                    };
                    try
                    {
                        smtp.Send(fromAddress, toAddress, subject, body);
                    }
                    catch
                    {
                        return RedirectToAction("Register_error");
                    }
                    return RedirectToAction("myprofile_bangla", "Home");
                }
                else
                {
                    ModelState.AddModelError(" ", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult password_retrival()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult password_retrival(string retrival_email)
        {
            if (string.IsNullOrEmpty(retrival_email))
            {
                Response.Redirect("~/Index/Home");
            }

            var x = db.userprofiles.Where(r => r.e_mail == retrival_email).ToList();
            if (x.Count > 0)
            {
                string username = x.Select(r => r.username).FirstOrDefault();
                string password = x.Select(r => r.password).FirstOrDefault();


                //send mail
                MailMessage message = new MailMessage();
                var fromAddress = "info@paymobd.com";
                var toAddress = retrival_email;
                const string fromPassword = "aa1613++";
                string subject = "Paymobd login informations";
                string body = string.Format("Your PayMoBD.com login informations are Username:{0} and Password:{1}.",
                    username, password);

                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.paymobd.com",
                    Port = 587,
                    EnableSsl = false,
                    Credentials = new NetworkCredential(fromAddress, fromPassword)
                };
                smtp.Send(fromAddress, toAddress, subject, body);
                //Thread.Sleep(2000);
                return PartialView("user_found");
            }
            else
            {
                Thread.Sleep(2000);
                return PartialView("no_user_found");
            }

        }

        public ActionResult Register_error()
        {
            return View();
        }
        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    var x = db.userprofiles
                        .Where(r => r.username == User.Identity.Name).FirstOrDefault();
                    x.password = model.NewPassword;
                    db.Entry(x).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Please check e-mail address and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
