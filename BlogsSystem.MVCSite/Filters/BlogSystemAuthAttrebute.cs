using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogsSystem.MVCSite.Filters
{
    public class BlogSystemAuthAttrebute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (!(filterContext.HttpContext.Request.Cookies["userName"] != null 
               || filterContext.HttpContext.Session["userName"] != null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary() {
                    { "controller","Home" },
                    { "action","login"}
                });
            }

            if (filterContext.HttpContext.Request.Cookies["userName"] != null&&
                filterContext.HttpContext.Session["userName"]==null)
            {
                filterContext.HttpContext.Session["userName"] = filterContext.HttpContext.Request.Cookies["userName"].Value;
                filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
            }
            //base.OnAuthorization(filterContext);
           
        }
    }
}