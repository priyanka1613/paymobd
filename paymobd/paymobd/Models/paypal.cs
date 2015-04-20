using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using paymobd.Models;

namespace paymobd.Models
{
    public class paypal
    {
        //decimal d;
        public static string paypal_pay(Transection tr)
        {

            decimal d = new decimal();
            //Converting String Money Value Into Decimal
            if (tr.transection_ammount <= 300)
            {
                decimal price = tr.transection_ammount + 60 + (tr.transection_ammount * 5 / 100);
                double x = double.Parse(price.ToString());
                double x1 = x / 75;
                string s = string.Format("{0:F}", x1);
                d = decimal.Parse(s);
            }
            else
            {
                decimal price = tr.transection_ammount + 60 + (tr.transection_ammount * 10 / 100);
                double x = double.Parse(price.ToString());
                double x1 = x / 75;
                string s = string.Format("{0:F}", x1);
                d = decimal.Parse(s);
            }
            //declaring empty String
            string returnURL = "";
            returnURL += "https://www.paypal.com/xclick/business=KDG32QWYAZ9PY";
            //PassingItem Name as dynamic
            returnURL += "&item_name=" + tr.mo_number;
            //Passing Amount as Dynamic
            returnURL += "&amount=" + d;
            //Passing Currency as your
            returnURL += "&currency=USD";
            //return Url if Customer wants To return to Previous Page
            returnURL += "&return=http://www.paymobd.com/Home/Thank_You";
            //return Url if Customer Wants To Cancel the Transaction
            returnURL += "&cancel_return=http://www.paymobd.com";
            //BIlling information prefilled 
            //AssigningName as Statically to Parameter
            returnURL += "&first_name=" + tr.username;
            returnURL += "&last_name=" + "Optional";
            returnURL += "&address1=" + "Optional";
            returnURL += "&address2=" + "Optional";
            returnURL += "&city=" + "Optional";
            returnURL += "&state=" + "Optional";
            returnURL += "&zip=" + "12345";
            returnURL += "&night_phone_a=" + "88";
            returnURL += "&night_phone_b=" + "01721234567";
            return returnURL;

        }
    }
}