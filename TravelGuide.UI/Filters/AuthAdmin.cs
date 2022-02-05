using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelGuide.UI.Models;

namespace TravelGuide.UI.Filters
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User != null && CurrentSession.User.Role.Name == "standart")
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
        }
    }
}