using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.service
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            var sessionIdCookie = httpContext.Request.Cookies["sessionid"];
            return sessionIdCookie != null;
        }
    }
}
    