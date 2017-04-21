using System;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using Owin;
using System.Security.Claims;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Claims;
using AzureWebApp.Models;
using Microsoft.Owin.Security;

namespace AzureWebApp.Auth
{
    public class OpenIdAuth : AbstractAuthConfigure
    {

        protected override void ConfigureInner(IAppBuilder app)
        {
            string Authority = this.Settings.AadInstance + "common";

            var options = new OpenIdConnectAuthenticationOptions
            {
                ClientId = this.Settings.ClientId,
                Authority = Authority,

                //********** ADD ************//
                RedirectUri = this.Settings.AppRedirectUri,
                //SignInAsAuthenticationType = "Cookies",
                //********** ADD ************//

                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                {
                    // 既定の検証 (基幹業務アプリケーションの場合と同じように、単一の発行者の値に対する検証) を使用する代わりに、
                    // 独自のマルチテナント検証ロジックを挿入します
                    //ValidateAudience = false,
                    ValidateIssuer = false,
                },
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = (context) =>
                    {

                        var id = context.AuthenticationTicket.Identity;

                        var givenName = id.FindFirst(System.Security.Claims.ClaimTypes.GivenName);
                        var upn = id.FindFirst(Microsoft.IdentityModel.Claims.ClaimTypes.Upn);
                        //var familyName = id.FindFirst(System.Security.Claims.ClaimTypes.FamilyName);
                        //var sub = id.FindFirst(System.Security.Claims.ClaimTypes.Subject);
                        var roles = id.FindAll(System.Security.Claims.ClaimTypes.Role);

                        // create new identity and set name and role claim type
                        var nid = new System.Security.Claims.ClaimsIdentity(
                            id.AuthenticationType,
                            System.Security.Claims.ClaimTypes.GivenName,
                            System.Security.Claims.ClaimTypes.Role);

                        //nid.AddClaim(givenName);
                        //nid.AddClaim(upn);
                        //nid.AddClaim(sub);
                        //nid.AddClaims(roles);

                        // add some other app specific claim
                        //nid.AddClaim(new System.Security.Claims.Claim("app_specific", "some data"));

                        //context.AuthenticationTicket = new AuthenticationTicket(
                        //    nid,
                        //    context.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    },
                    AuthorizationCodeReceived = (context) =>
                    {
                        var code = context.Code;

                        ClientCredential credential = new ClientCredential(this.Settings.ClientId, this.Settings.ClientSecret);
                        string tenantID = context.AuthenticationTicket.Identity.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
                        string signedInUserID = context.AuthenticationTicket.Identity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

                        AuthenticationContext authContext = new AuthenticationContext(this.Settings.AadInstance + tenantID, new ADALTokenCache(signedInUserID));
                        Task<AuthenticationResult> result = authContext.AcquireTokenByAuthorizationCodeAsync(
                            code, 
                            new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)), 
                            credential, 
                            this.Settings.GraphResourceID);


                        return Task.FromResult(0);
                    },
                    AuthenticationFailed = (context) =>
                    {
                        //context.OwinContext.Response.Redirect("/Home/AuthenticationError");
                        //context.HandleResponse(); // 例外を抑制します
                        return Task.FromResult(0);
                    }
                }
            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                // ASP.NET MVC の Authorize で使うためのクッキー。
                // 302 で承認コードをもらうときのレスポンスに入っている。
                CookieName = "webappCookie",
                // 下記設定を実施することで、Cookie による ASP.NET MVC の認証で、OpenIdConnect が必要となる。
                // 具体的には AccountController クラスになる。
                AuthenticationType = OpenIdConnectAuthenticationDefaults.AuthenticationType
            }
            );


            //app.UseCookieAuthentication()
            //CookieAuthenticationOptions o = new CookieAuthenticationOptions();


            app.UseOpenIdConnectAuthentication(options);
            //app.Map("/web", a => a.UseOpenIdConnectAuthentication(options));
        }
    }
}