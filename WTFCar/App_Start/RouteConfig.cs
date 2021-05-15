using System.Web.Mvc;
using System.Web.Routing;

namespace WTFCar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "DefaultNews",
            url: "News",
            defaults: new
            {
                controller = "News",
                action = "News",
                page = UrlParameter.Optional
            });


            routes.MapRoute(
            name: "DefaultIssuesJson",
            url: "Car/IssuesJSON/{issue}/{year}/{make}/{model}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "IssuesJSON",
                page = UrlParameter.Optional
            }



            );



            routes.MapRoute(
                name: "DefaultWorstMinivansByMakeModel",
                url: "Car/WorstMinivansByMakeModel/{page}",
                defaults: new
                {
                    controller = "Car",
                    action = "WorstMinivansByMakeModel",
                    page = UrlParameter.Optional
                }

            );

            routes.MapRoute(
            name: "DefaultWorstMinivansByYearMakeModel",
            url: "Car/WorstMinivansByYearMakeModel/{make}/{model}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstMinivansByYearMakeModel",
                make = UrlParameter.Optional,
                model = UrlParameter.Optional,
                page = UrlParameter.Optional

            }

            );

            routes.MapRoute(
            name: "DefaultWorstMinivansByYearMakeModelJSON",
            url: "Car/WorstMinivansByYearMakeModelJSON/{make}/{model}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstMinivansByYearMakeModelJSON",
                make = UrlParameter.Optional,
                model = UrlParameter.Optional,
                page = UrlParameter.Optional

            }

            );

            routes.MapRoute(
           name: "DefaultWorstSUVsByMakeModel",
           url: "Car/WorstSUVsByMakeModel/{page}",
           defaults: new
           {
               controller = "Car",
               action = "WorstSUVsByMakeModel",
               page = UrlParameter.Optional
           }

           );

            routes.MapRoute(
            name: "DefaultWorstSUVsByYearMakeModel",
            url: "Car/WorstSUVsByYearMakeModel/{make}/{model}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstSUVsByYearMakeModel",
                make = UrlParameter.Optional,
                model = UrlParameter.Optional,
                page = UrlParameter.Optional

            }

            );


            routes.MapRoute(
                name: "DefaultWorstSUVsByYearMakeModelJSON",
                url: "Car/WorstSUVsByYearMakeModelJSON/{make}/{model}/{page}",
                defaults: new
                {
                    controller = "Car",
                    action = "WorstSUVsByYearMakeModelJSON",
                    make = UrlParameter.Optional,
                    model = UrlParameter.Optional,
                    page = UrlParameter.Optional

                }

            );

            routes.MapRoute(
                name: "DefaultWorstCarsByYearMakeModelJSON",
                url: "Car/WorstCarsByYearMakeModelJSON/{make}/{model}/{page}",
                defaults: new
                {
                    controller = "Car",
                    action = "WorstCarsByYearMakeModelJSON",
                    make = UrlParameter.Optional,
                    model = UrlParameter.Optional,
                    page = UrlParameter.Optional

                }

            );

            routes.MapRoute(
                name: "DefaultWorstCarsByMakeModel",
                url: "Car/WorstCarsByMakeModel/{page}",
                defaults: new
                {
                    controller = "Car",
                    action = "WorstCarsByMakeModel",
                    page = UrlParameter.Optional
                }

            );

            routes.MapRoute(
                name: "DefaultWorstCarsByYearMakeModel",
                url: "Car/WorstCarsByYearMakeModel/{make}/{model}/{page}",
                defaults: new
                {
                    controller = "Car",
                    action = "WorstCarsMakeModelYears",
                    make = UrlParameter.Optional,
                    model = UrlParameter.Optional,
                    page = UrlParameter.Optional
                }

            );

            routes.MapRoute(
            name: "DefaultWorstTrucksByMakeModel",
            url: "Car/WorstTrucksByMakeModel/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstTrucksByMakeModel",
                page = UrlParameter.Optional
            }

            );

            routes.MapRoute(
            name: "DefaultWorstTrucksByYearMakeModel",
            url: "Car/WorstTrucksByYearMakeModel/{make}/{model}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstTrucksByYearMakeModel",
                make = UrlParameter.Optional,
                model = UrlParameter.Optional,
                page = UrlParameter.Optional

            }

            );

            routes.MapRoute(
            name: "DefaultWorstTrucksByYearMakeModelJSON",
            url: "Car/WorstTrucksByYearMakeModelJSON/{make}/{model}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstTrucksByYearMakeModelJSON",
                make = UrlParameter.Optional,
                model = UrlParameter.Optional,
                page = UrlParameter.Optional

            }

            );



            routes.MapRoute(
            name: "DefaultWorstCarsByMake",
            url: "Car/WorstCarsByMake/{make}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstCarsByMake",
                make = UrlParameter.Optional,
                page = UrlParameter.Optional
            }

            );

            //WorstCarMakesLastFiveYears

            routes.MapRoute(
             name: "DefaultWorstCarMakesLastFiveYears",
             url: "Car/WorstCarMakesLastFiveYears/{page}",
             defaults: new
             {
                 controller = "Car",
                 action = "WorstCarMakesLastFiveYears",
                 page = UrlParameter.Optional
             }
         );

            routes.MapRoute(
               name: "DefaultMakesByYear",
               url: "Car/GetMakesByYear/{year}",
               defaults: new
               {
                   controller = "Car",
                   action = "GetMakesByYear"
               }
           );

            routes.MapRoute(
              name: "DefaultModelsByYearMake",
              url: "Car/GetModelsFromYearMake/{year}/{make}",
              defaults: new
              {
                  controller = "Car",
                  action = "GetModelsFromYearMake"
              }
          );

            routes.MapRoute(
            name: "Default",
            url: "Car/WorstCars/{page}",
            defaults: new
            {
                controller = "Car",
                action = "WorstCars",
                page = UrlParameter.Optional
            }

           );

            routes.MapRoute(
          name: "DefaultWorstCarMakes",
          url: "Car/WorstCarMakes/{page}",
          defaults: new
          {
              controller = "Car",
              action = "WorstCarMakes",
              page = UrlParameter.Optional
          }

         );

            routes.MapRoute(
              name: "DefaultWorstCarsAllTimeSortYear",
              url: "Car/WorstCarsAllTime/{page}/{sortBy}/{yearBegin}/{yearEnd}",
              defaults: new
              {
                  controller = "Car",
                  action = "WorstCarsAllTime",
                  page = UrlParameter.Optional,
                  sortBy = UrlParameter.Optional,
                  yearBegin = UrlParameter.Optional,
                  yearEnd = UrlParameter.Optional
              }

            );

            routes.MapRoute(
              name: "DefaultWorstCarsAllTime",
              url: "Car/WorstCarsAllTime/{page}",
              defaults: new
              {
                  controller = "Car",
                  action = "WorstCarsAllTime",
                  page = UrlParameter.Optional
              }

            );



            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Car", action = "Index", id = UrlParameter.Optional }
            );




            routes.MapRoute(
               name: "Default2",
               url: "{controller}/{action}/{year}/{make}/{model}",
               defaults: new
               {
                   controller = "Car",
                   action = "Lookup"
               }
           );



            routes.MapRoute(
            name: "Default4",
            url: "{controller}/{action}/{issue}/{year}/{make}/{model}/{page}",
            defaults: new
            {
                controller = "Car",
                action = "Issues",
                page = UrlParameter.Optional
            }




            );





        }
    }
}
