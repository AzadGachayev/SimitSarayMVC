using System.Web.Mvc;

namespace Simit_Saray.Areas.SimitAdmin
{
    public class SimitAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SimitAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SimitAdmin_default",
                "SimitAdmin/{controller}/{action}/{id}",
                new { controller = "AdminHome", action = "Index", id = UrlParameter.Optional },
                new string[] { "Simit_Saray.Areas.SimitAdmin.Controllers" }

            );
        }
    }
}