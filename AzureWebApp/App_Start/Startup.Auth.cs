using Owin;
using AzureWebApp.Util;

namespace AzureWebApp
{
    public partial class Startup
    {

        public void ConfigureAuth(IAppBuilder app)
        {
            LoginManager.GetConfigure().Configure(app);
        }

    }
}
