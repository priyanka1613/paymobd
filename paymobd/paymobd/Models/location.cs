using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace paymobd.Models
{
    public class location
    {
        public DataTable GetLocation(string ipaddress)
        {

            WebRequest rssReq = WebRequest.Create("http://smart-ip.net/geoip-xml/" + ipaddress);
            //WebRequest rssReq = WebRequest.Create("http://freegeoip.appspot.com/xml/" + ipaddress);

            WebProxy px = new WebProxy("http://freegeoip.appspot.com/xml/" + ipaddress);
            //WebProxy px = new WebProxy("http://smart-ip.net/geoip-xml/" + ipaddress);
            rssReq.Proxy = px;
            rssReq.Timeout = 2000;
            try
            {
                WebResponse rep = rssReq.GetResponse();
                XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());
                DataSet ds = new DataSet();
                ds.ReadXml(xtr);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

        }
    }
}