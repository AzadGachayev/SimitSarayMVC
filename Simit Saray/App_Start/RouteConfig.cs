using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Simit_Saray
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                    name: "Abouts",
                    url: "haqqimizda",
                    defaults: new { controller = "Home", action = "about" },
                   namespaces: new string[] { "Simit_Saray.Controllers" }
                    );

            routes.MapRoute(
                   name: "Contacts",
                   url: "elaqe",
                   defaults: new { controller = "Home", action = "Contact" },
                  namespaces: new string[] { "Simit_Saray.Controllers" }
                   );

            routes.MapRoute(
               name: "Blog",
               url: "bloq",
               defaults: new { controller = "Home", action = "blog" },
              namespaces: new string[] { "Simit_Saray.Controllers" }
               );



            routes.Add("BlogItem", new SeoFriendlyRoute("bloq/{id}",
            new RouteValueDictionary(new { controller = "Home", action = "BlogItem" }),
            new MvcRouteHandler()));

            //  routes.MapRoute(
            //    name: "BlogDetail",
            //    url: "bloq/{id}/{*title}",
            //    defaults: new { controller = "Home", action = "BlogItem", id = UrlParameter.Optional },
            //     namespaces: new string[] { "Simit_Saray.Controllers" }
            //);


            routes.MapRoute(
                 name: "Gallery",
                 url: "qalereya",
                 defaults: new { controller = "Home", action = "Gallery" },
                namespaces: new string[] { "Simit_Saray.Controllers" }
                 );

            routes.MapRoute(
            name: "MenyuAll",
            url: "menyu",
            defaults: new { controller = "Home", action = "Menyu", },
             namespaces: new string[] { "Simit_Saray.Controllers" }
        );
        

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "Simit_Saray.Controllers" }
            );

        
        }
    }
}
