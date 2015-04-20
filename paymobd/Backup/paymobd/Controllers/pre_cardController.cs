using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paymobd.Models;

namespace paymobd.Controllers
{
    public class pre_cardController : Controller
    {
        private PayMoDb db = new PayMoDb();

        //
        // GET: /pre_card/

        public ActionResult Index()
        {
            var v = db.pre_cards.OrderBy(r => r.operator_name).ToList();
            var v1 = v.OrderBy(r => r.pre_ammount).ToList();
            return View(v1);
        }

        //
        // GET: /pre_card/Details/5

        public ActionResult Details(int id = 0)
        {
            pre_card pre_card = db.pre_cards.Find(id);
            if (pre_card == null)
            {
                return HttpNotFound();
            }
            return View(pre_card);
        }

        //
        // GET: /pre_card/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /pre_card/Create

        [HttpPost]
        public ActionResult Create(pre_card pre_card)
        {
            if (ModelState.IsValid)
            {
                db.pre_cards.Add(pre_card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(pre_card);
        }

        //
        // GET: /pre_card/Edit/5

        public ActionResult Edit(int id = 0)
        {
            pre_card pre_card = db.pre_cards.Find(id);
            if (pre_card == null)
            {
                return HttpNotFound();
            }
            return View(pre_card);
        }

        //
        // POST: /pre_card/Edit/5

        [HttpPost]
        public ActionResult Edit(pre_card pre_card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pre_card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pre_card);
        }

        //
        // GET: /pre_card/Delete/5

        public ActionResult Delete(int id = 0)
        {
            pre_card pre_card = db.pre_cards.Find(id);
            if (pre_card == null)
            {
                return HttpNotFound();
            }
            return View(pre_card);
        }

        //
        // POST: /pre_card/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            pre_card pre_card = db.pre_cards.Find(id);
            db.pre_cards.Remove(pre_card);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //buy prepaid cards
        public ActionResult buy_prepaid_cards()
        {
            //List<string> operators = new List<string>();
            var v = db.pre_cards.Select(r => r.operator_name).Distinct().ToList();
            ViewBag.operators = v;
            return PartialView();
        }
        [HttpPost]
        public ActionResult buy_prepaid_cards(string operator_name)
        {
            string v = db.pre_cards.Where(r => r.operator_name == operator_name).Select(r => r.pre_number).FirstOrDefault();
            //List<double> card_amount = new List<double>();
            ViewBag.card_number = v;

            //remove card number
            var s = db.pre_cards.Where(r => r.operator_name == operator_name).ToList();
            var s1 = s.Where(r => r.pre_number == v).FirstOrDefault();
            db.pre_cards.Remove(s1);

            //deducting wallet money
            var x = db.userprofiles.Where(r => r.username == User.Identity.Name).FirstOrDefault();
            x.wallet_money = x.wallet_money - 50;
            db.Entry(x).State = EntityState.Modified;

            //adding transection history
            transection_final tf = new transection_final();
            tf.ammount = 50;
            tf.send_to = v;
            tf.sent_date = DateTime.Now;
            tf.status = "Prepaid card";
            tf.username = User.Identity.Name;
            db.transection_finals.Add(tf);
            db.SaveChanges();
            db.SaveChanges();
            return PartialView("buy_prepaid_cards1");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}