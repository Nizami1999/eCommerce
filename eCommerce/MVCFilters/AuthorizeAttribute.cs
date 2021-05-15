using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.MVCFilters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["AdminLogged"] == null ||
                (bool)HttpContext.Current.Session["AdminLogged"] != true)
            {
                filterContext.Result = new RedirectResult("/Admin/Account/Login");
            }
        }
    }
}