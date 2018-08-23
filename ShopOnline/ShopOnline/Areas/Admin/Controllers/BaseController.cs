using ShopOnline.Commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserSession)Session[CommondConstant.USER_SESSION];
            if (session == null)
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Login", action = "Index", Area = "Admin" }));
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type.Equals("success"))
                TempData["AlertType"] = "alert-success";
            else if (type.Equals("warning"))
                TempData["AlertType"] = "alert-warning";
            else
                TempData["AlertType"] = "alert-danger";
        }
    }
}