using AzureWebApp.Handler;
using System.Web.Http;

namespace AzureWebApp.Controllers
{
    public class BaseWebApi  : ApiController
    {
        private static RequestHandler handler = new RequestHandler();

        public RequestHandler RequestHandler
        {
            get
            {
                return handler;
            }
        }
    }
}