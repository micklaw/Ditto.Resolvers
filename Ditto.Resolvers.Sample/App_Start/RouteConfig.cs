using System.Web.Mvc;
using System.Web.Routing;

namespace Ditto.Resolvers.Sample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}