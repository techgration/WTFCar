using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;
using WTFCar.Models;

namespace WTFCar.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult News()
        {

            //get the motor one articles
            String RSSURL = "https://www.motor1.com/rss/articles/all/";

            WebClient wclient = new WebClient();
            string RSSData = wclient.DownloadString(RSSURL);

            XDocument xml = XDocument.Parse(RSSData);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = ((string)x.Element("pubDate")).Substring(0, 16)
                               });
            ViewBag.RSSFeed = RSSFeedData;
            ViewBag.URL = RSSURL;

            //get the recalls
            string RSSURL2 = "https://www-odi.nhtsa.dot.gov/rss/feeds/rss_recalls_V.xml";
            string RSSData2 = wclient.DownloadString(RSSURL2);

            xml = XDocument.Parse(RSSData2);
            var RSSFeedData2 = (from x in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = ((string)x.Element("pubDate")).Substring(0, 16)
                               });

            ViewBag.RSSFeed2 = RSSFeedData2;
            ViewBag.URL2 = RSSURL2;

            return View();

        }

    }


}