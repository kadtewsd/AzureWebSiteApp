using System.Configuration;

namespace AzureWebApp.Util
{
    public class FBConfigurationSettings  : AbstractConfigurationSettings
    {

        public override string ClientId
        {
            get
            {
                return ConfigurationManager.AppSettings["FB:ClientId"];
            }
        }

        public override string ClientSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["FB:ClientSecret"];
            }
        }

        public override string AadInstance
        {
            get
            {
                return ConfigurationManager.AppSettings["FB:AADInstance"];
            }
        }

        //// This is the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
        public override string GraphResourceID
        {
            get
            {
                return "https://graph.facebook.com/";
            }
        }

        public override string TenantID
        {
            get
            {
                return System.Security.Claims.ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            }
        }

        public override string UserObjectID
        {
            get
            {
                return System.Security.Claims.ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            }
        }
    }
}