using AzureWebApp.Handler;
using System.Web.Mvc;

namespace AzureWebApp.Controllers
{
    public class BaseController : Controller
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