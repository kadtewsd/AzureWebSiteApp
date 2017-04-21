using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(webappWebApp.Startup))]
namespace AzureWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}