using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WTFCar.Models;

namespace WTFCar.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LookupJSON(string year, string make, string model)
        {
            if (year != null && make != null && model != null)
            {
                Data d = new Data();
                List<WTFC> currentCars = d.GetCurrentCarInfo(year, make, model);
                return Json(currentCars, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }


        }

        // GET: Car/Details/5
        public ActionResult Lookup(string year, string make, string model)
        {
            if (year != null && make != null && model != null)
            {
                Data d = new Data();
                List<WTFC> currentCars = d.GetCurrentCarInfo(year, make, model);
                if (currentCars != null && currentCars.Count > 0)
                {
                    ViewBag.WTFC = currentCars[0];

                    ViewBag.Year = year;
                    ViewBag.Make = make;
                    ViewBag.Model = model;

                    ViewBag.Title = "WTF Car";
                    ViewBag.Subtitle = year + " " + make + " " + model;

                    ViewBag.MetaDescription = "The " + year + " " + make + " " + model + " has " + currentCars[0].ComplaintCount + " total complaints." + " The total WTF points for this vehicle are " + currentCars[0].Totalpts;
                    foreach (WTFC w in currentCars)
                    {
                        ViewBag.MetaKeywords += w.year + "," + w.make + "," + w.model + ",";
                        if (w != null && w.WordCloudList != null)
                        {
                            foreach (WordCloud wc in w.WordCloudList)
                            {
                                ViewBag.MetaKeywords += wc.Word + ",";
                            }
                        }

                    }
                }


            }
            else
            {
                ViewBag.Year = "";
                ViewBag.Make = "";
                ViewBag.Model = "";

                ViewBag.Title = "WTF Car";
                ViewBag.Subtitle = "Lookup Vehicle";

                ViewBag.MetaDescription = "Lookup vehicles on the WTFCar.com web site by Year, Make, Model";
                ViewBag.MetaKeywords = "WTF, Car, Lookup, Year, Make, Model";


                ViewBag.WTFC = null;

            }

            return View();
        }

        public ActionResult IssuesJSON(string issue, string year, string make, string model, string page)
        {
            if (issue != null && year != null && make != null && model != null)
            {
                Data d = new Data();

                CarComments complaints = d.GetIssuesByComponent(page, issue, year, make, model);

                return Json(complaints, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        // GET: Car/Details/5
        public ActionResult Issues(string issue, string year, string make, string model, string page)
        {
            if (issue != null && year != null && make != null && model != null)
            {
                Data d = new Data();

                int complaintCountTotal = d.GetComplaintCountTotal(issue, year, make, model);

                CarComments complaints = d.GetIssuesByComponent(page, issue, year, make, model);
                ViewBag.Complaints = complaints;

                ViewBag.Year = year;
                ViewBag.Make = make;
                ViewBag.Model = model;
                ViewBag.Issue = issue;
                ViewBag.Page = page;

                ViewBag.ComplaintCount = complaintCountTotal;

                ViewBag.Title = "WTF Car";
                ViewBag.Subtitle = year + " " + make + " " + model + " " + issue + " issues";

                ViewBag.MetaDescription = "The " + year + " " + make + " " + model + " has " + complaintCountTotal + " " + issue.ToLower() + " complaints.";
                ViewBag.MetaKeywords = year + "," + make + "," + model + "," + issue.ToLower() + ",complaints,wtf,car";

            }
            else
            {
                ViewBag.Year = "";
                ViewBag.Make = "";
                ViewBag.Model = "";

                ViewBag.Complaints = new CarComments();

                ViewBag.Title = "WTF Car";
                ViewBag.Subtitle = year + " " + make + " " + model + " " + " issues";

                ViewBag.MetaDescription = "The " + year + " " + make + " " + model + " " + issue.ToLower() + " complaints.";
                ViewBag.MetaKeywords = year + "," + make + "," + model + "," + ",complaints,wtf,car";
            }

            return View();
        }

        public ActionResult WorstCarsByMakeJSON(string make, string page)
        {
            if (!String.IsNullOrEmpty(make))
            {
                Data d = new Data();
                List<WTFC> currentCars = d.GetWorstCarsByMake(make, ref page);

                return Json(currentCars, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
           
        }

        // GET: Car/Details/5
        public ActionResult WorstCarsByMake(string make, string page)
        {

            if (String.IsNullOrEmpty(page))
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> currentCars = d.GetWorstCarsByMake(make, ref page);

            if (currentCars == null || currentCars.Count == 0)
            {
                while (currentCars == null && currentCars.Count == 0 && page != "" && page != "0")
                {
                    int pageInt = (Convert.ToInt32(page) - 1);
                    page = pageInt.ToString();
                }
                currentCars = d.GetWorstCarsByMake(make, ref page);
            }


            ViewBag.WorstCars = currentCars;

            ViewBag.WTFC = currentCars[0];

            ViewBag.Make = make;
            ViewBag.Year = "";
            ViewBag.Model = "";

            ViewBag.Title = "WTF Car - Worst Cars By Make - ";
            ViewBag.Subtitle = make;

            ViewBag.MetaDescription = "Worst Car Makes - " + make + " complaints";
            ViewBag.MetaKeywords = make + "," + ",complaints,wtf,car";

            return View();
        }

        public ActionResult WorstCars(string page)
        {
            if (page == null || page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstCars(page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Page = Convert.ToInt32(page);

            ViewBag.Title = "WTF Car - Worst Cars - ";
            ViewBag.Subtitle = "Search/Sort Most WTF Cars";

            ViewBag.MetaDescription = "WTF Car - Worst Cars. Search, Sort by WTF Points and Year";

            foreach (WTFC w in worstCars)
            {
                ViewBag.MetaKeywords += w.year + "," + w.make + "," + w.model + ",";
                if (w != null && w.WordCloudList != null)
                {
                    foreach (WordCloud wc in w.WordCloudList)
                    {
                        ViewBag.MetaKeywords += wc.Word + ",";
                    }
                }
            }

            return View();
        }

        public ActionResult WorstCarsAllTime(string page, string sortBy, string yearBegin, string yearEnd)
        {
            if (page == null || page == "")
            {
                page = "1";
            }

            if (sortBy == null || sortBy == "")
            {
                sortBy = "";
            }

            if (yearBegin == null || yearBegin == "")
            {
                yearBegin = "";
            }

            if (yearEnd == null || yearEnd == "")
            {
                yearBegin = "";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstCarsAllTime(sortBy, yearBegin, yearEnd, page);

            if (worstCars == null || worstCars.Count == 0)
            {
                while (worstCars == null || worstCars.Count == 0 || page != "" || page != "0")
                {
                    int pageInt = (Convert.ToInt32(page) - 1);
                    page = pageInt.ToString();
                    worstCars = d.GetWorstCarsAllTime(sortBy, yearBegin, yearEnd, page);
                }

            }

            ViewBag.WorstCars = worstCars;

            ViewBag.SortBy = sortBy;
            ViewBag.YearBegin = yearBegin;
            ViewBag.YearEnd = yearEnd;

            ViewBag.Page = Convert.ToInt32(page);

            ViewBag.Title = "WTF Car - Worst Cars";
            ViewBag.Subtitle = "Search/Sort";

            ViewBag.MetaDescription = "WTF Car - Worst Cars of All Time";

            foreach (WTFC w in worstCars)
            {
                ViewBag.MetaKeywords += w.year + "," + w.make + "," + w.model + ",";
                if (w != null && w.WordCloudList != null)
                {
                    foreach (WordCloud wc in w.WordCloudList)
                    {
                        ViewBag.MetaKeywords += wc.Word + ",";
                    }
                }
            }


            return View();
        }

        public ActionResult WorstCarMakes(string page)
        {
            if (page == null || page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstCarMakes(ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Page = Convert.ToInt32(page);

            ViewBag.Title = "WTF Car";
            ViewBag.Subtitle = "Worst Car Makes";

            ViewBag.MetaDescription = "WTF Car - Worst Car Makes";

            foreach (WTFC w in worstCars)
            {
                ViewBag.MetaKeywords += w.year + "," + w.make + "," + w.model + ",";
                if (w != null && w.WordCloudList != null)
                {
                    foreach (WordCloud wc in w.WordCloudList)
                    {
                        ViewBag.MetaKeywords += wc.Word + ",";
                    }
                }
            }

            return View();
        }

        public ActionResult WorstCarMakesLastFiveYears(string page)
        {
            if (page == null || page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstCarMakesLastFiveYears(ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Page = Convert.ToInt32(page);

            ViewBag.Title = "WTF Car - Worst Cars";
            ViewBag.Subtitle = "Search/Sort Most WTF Cars";

            ViewBag.MetaDescription = "WTF Car - Worst Cars of the last 5 years";

            foreach (WTFC w in worstCars)
            {
                ViewBag.MetaKeywords += w.year + "," + w.make + "," + w.model + ",";
                if (w != null && w.WordCloudList != null)
                {
                    foreach (WordCloud wc in w.WordCloudList)
                    {
                        ViewBag.MetaKeywords += wc.Word;
                    }
                }

            }

            return View();
        }

        public ActionResult WorstCarMakeModels()
        {
            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstCarMakeModels();
            ViewBag.WorstCars = worstCars;

            ViewBag.Title = "Worst Vehicles - Make & Model";

            return View();
        }

        public ActionResult WorstCarsMakeModelYears(string make, string model, string page)
        {
            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstCarsMakeModelYears(make, model, ref page);
            ViewBag.WorstCars = worstCars;

            if (make != "" && model != "")
            {
                ViewBag.Title = "Worst Vehicles - " + make + " " + model;
            }
            else
            {
                ViewBag.Title = "Worst Vehicles - Make, Model & Year";
            }


            ViewBag.Page = page;

            return View();
        }

        public ActionResult WorstTrucksByMakeModel(string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstTrucksByMakeModel(ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Title = "Worst Trucks By Make/Model";


            ViewBag.Page = page;

            return View();
        }

        public ActionResult WorstTrucksByYearMakeModel(string make, string model, string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstTrucksByYearMakeModel(make, model, ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Title = "Worst Trucks - " + make + " " + model;


            ViewBag.Page = page;

            return View();
        }

        public ActionResult WorstSUVsByMakeModel(string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstSUVsByMakeModel(ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Title = "Worst SUVs by Make/Model";

            ViewBag.Page = page;

            return View();
        }

        public ActionResult WorstSUVsByYearMakeModel(string make, string model, string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstSUVsByYearMakeModel(make, model, ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Title = "Worst SUVs - " + make + " " + model;


            ViewBag.Page = page;

            return View();
        }

        public ActionResult WorstTrucksByMakeModelJSON(string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstTrucksByMakeModel(ref page);
            return Json(worstCars, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorstTrucksByYearMakeModelJSON(string make, string model, string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstTrucksByYearMakeModel(make, model, ref page);

            return Json(worstCars, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorstSUVsByMakeModelJSON(string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstSUVsByMakeModel(ref page);
            return Json(worstCars, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorstSUVsByYearMakeModelJSON(string make, string model, string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstSUVsByYearMakeModel(make, model, ref page);
            return Json(worstCars, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorstMinivansByMakeModel(string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstMinivansByMakeModel(ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Title = "Worst Minivans by Make/Model";

            ViewBag.Page = page;

            return View();
        }

        public ActionResult WorstMinivansByYearMakeModel(string make, string model, string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstMinivansByYearMakeModel(make, model, ref page);
            ViewBag.WorstCars = worstCars;

            ViewBag.Title = "Worst Minivans - " + make + " " + model;
            
            ViewBag.Page = page;

            return View();
        }

        public ActionResult WorstMinivansByMakeModelJSON(string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();

            List<WTFC> worstCars = d.GetWorstMinivansByMakeModel(ref page);

            return Json(worstCars, JsonRequestBehavior.AllowGet);


        }

        public ActionResult WorstMinivansByYearMakeModelJSON(string make, string model, string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstMinivansByYearMakeModel(make, model, ref page);

            return Json(worstCars, JsonRequestBehavior.AllowGet);

        }

        public ActionResult WorstCarsByMakeModelJSON(string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();

            List<WTFC> worstCars = d.GetWorstCarsByMakeModel(ref page);

            return Json(worstCars, JsonRequestBehavior.AllowGet);


        }

        public ActionResult WorstCarsByYearMakeModelJSON(string make, string model, string page)
        {
            if (page == "")
            {
                page = "1";
            }

            Data d = new Data();
            List<WTFC> worstCars = d.GetWorstCarsByYearMakeModel(make, model, ref page);

            return Json(worstCars, JsonRequestBehavior.AllowGet);

        }




        public ActionResult GetMakesByYear(string year)
        {
            Data d = new Data();

            return Json(d.GetMakesFromYear(year), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModelsFromYearMake(string year, string make)
        {
            Data d = new Data();

            return Json(d.GetModelsFromYearMake(year, make), JsonRequestBehavior.AllowGet);
        }




    }
}
