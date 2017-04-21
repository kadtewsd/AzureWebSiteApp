using AzureWebApp.stub;
using System.Configuration;

namespace AzureWebApp.Util
{
    public abstract class AbstractConfigurationSettings
    {

        public abstract string ClientId
        {
            get;
        }

        public abstract string ClientSecret
        {
            get;
        }

        public abstract string AadInstance
        {
            get;
        }

        //// This is the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
        public abstract string GraphResourceID
        {
            get;
        }

        public abstract string TenantID
        {
            get;
        }

        public abstract string UserObjectID
        {
            get;
        }

        public string AppRedirectUri
        {
            get
            {
                return DebugUtil.IsDebug() ? "https://localhost:44300/" : "https://webappwebapp1.azurewebsites.net/";
            }
        }

        public  string DBConnectionName
        {
            get
            {
                return DebugUtil.IsDebug() ? "webappTestDB" : "webappDB";
            }
        }

        public  string DBConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DBConnectionName].ConnectionString;
            }
        }

    }
}
