using AzureWebApp.Util;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace AzureWebApp.Auth
{
    public abstract class AbstractAuthConfigure
    {
        static AbstractAuthConfigure()
        {
            settings = LoginManager.GetSettings();
        }

        private static AbstractConfigurationSettings settings = null;

        protected AbstractConfigurationSettings Settings
        {
            get
            {
                return settings;
            }
        }


        public void Configure(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            this.ConfigureInner(app);
        }

        protected abstract void ConfigureInner(IAppBuilder app);
    }
}
