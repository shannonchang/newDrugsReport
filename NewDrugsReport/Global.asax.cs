using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NewDrugs.App_Start;

namespace NewDrugs
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = false;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpResponse resp = HttpContext.Current.Response;
            resp.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}
