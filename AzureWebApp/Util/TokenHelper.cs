using System.Security.Claims;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using AzureWebApp.Models;
using System.Web;

namespace AzureWebApp.Util
{
    public  class TokenHelper : Controller
    {

        public const string FB_User_Id_Claim_Type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public const string FB_Access_Token_Claim_Type = "urn:facebook:access_token";

        public async Task<string> GetTokenForApplication()
        {
            AbstractConfigurationSettings settings = LoginManager.GetSettings();

            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string tenantID = settings.TenantID;
            string userObjectID = settings.UserObjectID;

            // get a token for the Graph without triggering any user interaction (from the cache, via multi-resource refresh token, etc)
            ClientCredential clientcred = new ClientCredential(settings.ClientId, settings.ClientSecret);
            // initialize AuthenticationContext with the token cache of the currently signed in user, as kept in the app's database
            AuthenticationContext authenticationContext = new AuthenticationContext(settings.AadInstance + tenantID, new ADALTokenCache(signedInUserID));
            AuthenticationResult authenticationResult = null;
            try
            {
                authenticationResult = await authenticationContext.AcquireTokenAsync(
                    settings.GraphResourceID,
                     clientcred
                     );
                     //new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
                
            } catch (AdalException e)
            {
                //TODO ユーザーのアクセストークンをキャッシュからクリア
                authenticationContext.TokenCache.Clear();
                ADALTokenCache cache = new ADALTokenCache(signedInUserID);
                cache.Clear();                
                throw e;
            }
            //AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenSilentAsync(KasakaidConfigurationManager.GraphResourceID, clientcred, new UserIdentifier(userObjectID, UserIdentifierType.OptionalDisplayableId));
            return authenticationResult.AccessToken;
        }

        public void RefreshSession()
        {
            HttpContext.GetOwinContext().Authentication.Challenge(
                new AuthenticationProperties { RedirectUri = "/UserProfile" },
                OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }

    }
}