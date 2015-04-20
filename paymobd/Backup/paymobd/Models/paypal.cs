using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using paymobd.Models;

namespace paymobd.Models
{
    public class paypal
    {
        public static string paypal_pay(Transection tr)
        {
            //Converting String Money Value Into Decimal
            decimal price = tr.transection_ammount + 60+(tr.transection_ammount*5/100);
            double x = double.Parse(price.ToString());
            double x1 = x / 75;
            string s = string.Format("{0:F}", x1);
            decimal d = decimal.Parse(s);
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
            returnURL += "&return=http://www.bgdtech.com/Home/Thank_You";
            //return Url if Customer Wants To Cancel the Transaction
            returnURL += "&cancel_return=http://www.bgdtech.com";
            //BIlling information prefilled 
            //AssigningName as Statically to Parameter
            returnURL += "&first_name=" + "FirstName(optional)";
            returnURL += "&last_name=" + "LastName(optional)";
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