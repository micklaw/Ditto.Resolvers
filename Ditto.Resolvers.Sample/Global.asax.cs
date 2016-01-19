using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;

namespace Ditto.Resolvers.Sample
{
    public class MvcApplication : UmbracoApplication
    {
        protected override void OnApplicationStarting(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}