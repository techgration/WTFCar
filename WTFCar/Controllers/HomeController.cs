using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WTFCar.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "WTF Car is an innovative car problem tracking solution. We track trends occuring in the automotive repair industry.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us! Coming soon...";

            return View();
        }
    }
}