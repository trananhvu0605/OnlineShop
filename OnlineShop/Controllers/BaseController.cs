using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session[CommonConstants.USERCLIENT_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                  RouteValueDictionary(new { Controller = "Login", Action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}