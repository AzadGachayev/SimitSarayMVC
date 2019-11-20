using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{
    public class AuthorizationFilter
    {
    }
    public class AuthorizationFilterController: AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true))
            {
                //Do not check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }
            //Check for authorization
            if(HttpContext.Current.Session["activeAdmin"] == null)
            {
                filterContext.Result = new RedirectResult("~/SimitAdmin/AdminAccount/Login");
            }

        }

    }
}