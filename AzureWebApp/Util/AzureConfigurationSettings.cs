using AzureWebApp.Models;
using AzureWebApp.stub;
using Microsoft.IdentityModel.Claims;
using System.Configuration;

namespace AzureWebApp.Util
{
    public class AzureConfigurationSettings  : AbstractConfigurationSettings
    {

        public override string ClientId
        {
            get
            {
                return ConfigurationManager.AppSettings["ida:ClientId"];
            }
        }

        public override string ClientSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ida:ClientSecret"];
            }
        }

        public override string AadInstance
        {
            get
            {
                return ConfigurationManager.AppSettings["ida:AADInstance"];
            }
        }

        //// This is the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
        public override string GraphResourceID
        {
            get
            {
                //return "https://graph.microsoft.com";
                return "https://graph.windows.net"; // Insufficient Proviledge
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