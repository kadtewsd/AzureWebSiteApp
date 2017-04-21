using AzureWebApp.Models.UserInfo;
using AzureWebApp.Service.UserInfo;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AzureWebApp.Controllers.UserInfo
{
    [Authorize]
    public class UserInfoController : Controller
    {
        private IUserInfoService service = null;

        [Dependency]
        public IUserInfoService UserInfoService
        {
            set
            {
                this.service = value;
            }
        }
        // GET: NextStep
        public ActionResult GetIntegTeamList(string[] alias)
        {
            IList<UserInfoModel> result = new List<UserInfoModel>();
            UserInfoModel info = service.GetUserInfo(alias[0]);
            result.Add(info);
            return View(result);
        }
    }
}