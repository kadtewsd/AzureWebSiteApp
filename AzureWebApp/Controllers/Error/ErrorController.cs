using AzureWebApp.Models.Model;
using System.Web.Mvc;

namespace AzureWebApp.Controllers.UserInfo
{
    public class ErrorController : Controller
    {
        // GET: NextStep
        public ActionResult MsUserNotFound(FirstModel model)
        {
            //this.ViewBag.Alias = model.Alias;
            return View(model);
        }

        public ActionResult BirthDayNotAllowed()
        {
            return View();
        }
    }
}