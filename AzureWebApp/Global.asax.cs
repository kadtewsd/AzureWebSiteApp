using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using AzureWebApp.App_Start;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace AzureWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //NuGet Microsoft.AspNet.WebApi
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Initialise();

            // log4Net settings
            AppConfig.Configure();
        }
    }
}
