using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Facebook;
using AzureWebApp.Util;

namespace AzureWebApp.Auth
{
    public class FBAuth : AbstractAuthConfigure
    {

        protected override void ConfigureInner(IAppBuilder app)
        {
            var options = new FacebookAuthenticationOptions
            {
                AppId = this.Settings.ClientId,
                AppSecret = this.Settings.ClientSecret,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                //CallbackPath = new PathString("/"),

                //http://stackoverflow.com/questions/18942196/how-to-access-facebook-private-information-by-using-asp-net-identity-owin
                Provider = new FacebookAuthenticationProvider
                {
                    OnReturnEndpoint = (context) =>
                    {
                        return Task.FromResult(0);
                    },

                   OnAuthenticated = (context) =>
                    {
                        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
                        // Retrieve the OAuth access token to store for subsequent API calls
                        string accessToken = context.AccessToken;

                        // Retrieve the username
                        string facebookUserName = context.Name;

                        // 後で使うようにアクセストークンをクレームに入れておく。
                        context.Identity.AddClaim(new System.Security.Claims.Claim(TokenHelper.FB_Access_Token_Claim_Type, context.AccessToken, XmlSchemaString, "Facebook"));

                        // You can even retrieve the full JSON-serialized user
                        var serializedUser = context.User;
                        // avoid compile alert which is async method.
                        return Task.FromResult(0);
                    }
                }

            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions {});

            // 取得できる項目
            //options.Scope.Add("email");
            options.Scope.Add("user_birthday");
            FacebookAuthenticationExtensions.UseFacebookAuthentication(app, options);
        }
    }
}