using AzureWebApp;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace webappWebAppTests.Controllers
{
    [TestClass()]
    public abstract class BaseApiTestFixture : IDisposable
    {

        private const string baseAddress = "https://localhost:44300";
        private static IDisposable _webApp;
        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            _webApp = WebApp.Start<Startup>(baseAddress);
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            _webApp.Dispose();
        }

        protected BaseApiTestFixture()
        {
            // Normally you'd create the server with:
            //
            //    Server = TestServer.Create<Startup>();
            //
            // but in this case we need to get hold of a DataProtector that can be 
            // used to generate compatible OAuth tokens.
            Server = TestServer.Create(app =>
            {
                Startup startup = new Startup();
                startup.Configuration(app);
                
                this.DataProtector = 
                    Microsoft.Owin.Security.DataProtection.AppBuilderExtensions.CreateDataProtector(
                            app, typeof(Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1");


                // リクエスト先をひろう
                //app.Run(async context =>
                //{
                //    if (context.Request.Headers.Get("Header1") == "HeaderValue1")
                //    {
                //        await context.Response.WriteAsync("Hello world using OWIN TestServer");
                //    }
                //    else
                //    {
                //        await context.Response.WriteAsync("Header missing");
                //    }
                //});
            }
            );

            Server.BaseAddress = new Uri(baseAddress);
        }

        
        protected abstract string RequestUri
        {
            get;
        }

        [TestInitialize]
        public  void Initialize()
        {
            AzureWebApp.Bootstrapper.Initialise();
        }

        protected IDataProtector DataProtector { get; set; }

        //protected HttpClient HttpsClient
        //{
        //    get
        //    {
        //        var client = new HttpClient(Server.Handler) { BaseAddress = new Uri(baseAddress) };
        //        return client;
        //    }
        //}

        protected virtual string UriParameters
        {
            get 
            {
                return "";
            } 
        }

        protected virtual string QueryString
        {
            get
            {
                return "";
            }
        }

        protected string AccessToken
        {
            get; set;
        }

        private string _token;

        protected virtual string Token
        {
            get { return _token ?? (_token = GenerateToken()); }
        }

        private TestServer Server { get; set; }

        protected RequestBuilder CreateRequest(HttpMethod method, object data)
        {
            this.AccessToken = this.GenerateToken();
            
            var request = Server.CreateRequest(this.RequestUri + QueryString);

            if (!String.IsNullOrEmpty(this.AccessToken))
            {
                request.AddHeader("Authorization", "Bearer " + this.AccessToken);
            }
            System.Diagnostics.Debug.WriteLine("Created request: {0} {1} {2} {3} {4} ", method, Server.BaseAddress, this.RequestUri, UriParameters, QueryString);
            return request;

        }

        public async Task<string> DoTest()
        {

            string ret = null;
            HttpResponseMessage response = null;
            try
            {
                response = await GetAsync();

                using (var httpClient = new HttpClient())
                {
                    var accessToken = this.AccessToken;
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", accessToken);
                    var requestUri = new Uri(this.Server.BaseAddress + this.RequestUri);
                    response =  await httpClient.GetAsync(requestUri);
                }
                ret = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message + "  \n" +  e.StackTrace);
                Assert.Fail();
                throw e;
       
            }

            return ret;
        }

        protected virtual async Task<HttpResponseMessage> GetAsync()
        {

            var result = CreateRequest(HttpMethod.Get, null);
            return await result.GetAsync();
        }

        protected virtual async Task<TResult> GetAsync<TResult>()
        {
            var response = await GetAsync();
            return await response.Content.ReadAsAsync<TResult>();
        }

        private string GenerateToken()
        {

            // Generate an OAuth bearer token for ASP.NET/Owin Web Api service that uses the default OAuthBearer token middleware.

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "admin@kkproj15.onmicrosoft.com"),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Role, "PowerUser"),
            };

            var identity = new ClaimsIdentity(claims, "Test");

            // Use the same token generation logic as the OAuthBearer Owin middleware. 

            var tdf = new TicketDataFormat(this.DataProtector);
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddHours(1) });
            var accessToken = tdf.Protect(ticket);

            return accessToken;
        }

        public virtual void Dispose()
        {
            if (Server != null)
            {
                Server.Dispose();
            }
        }
    }
}
