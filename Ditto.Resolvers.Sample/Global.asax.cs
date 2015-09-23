using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Ditto.Resolvers.Sample.Models.Ditto.TypeConverters;
using Our.Umbraco.Ditto;
using Our.Umbraco.Ditto.Resolvers.Container;
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

            DittoResolverTypeLocator.Register<DittoHtmlStringConverter>("rte");
            DittoResolverTypeLocator.Register<DittoHtmlStringConverter>("embed");
            DittoResolverTypeLocator.Register<GridImageConverter>("media");
        }
    }
}