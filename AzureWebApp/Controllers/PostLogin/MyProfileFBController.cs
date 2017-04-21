using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web;
using AzureWebApp.Service.PostLogin;
using System.Linq;
using System.Security.Claims;
using log4net;
using AzureWebApp.Models.Model;
using AzureWebApp.Util;

namespace AzureWebApp.Controllers.PostLogin
{
    //[RouteArea("web")]
    [Authorize]
    public class MyProfileFBController : Controller
    {
        private static ILog log = log4net.LogManager.GetLogger("IBatisNet");

        IMyProfileService service = null;
        // コンストラクターを使うと、Bootstrap でオブジェクトが生成できない。
        public MyProfileFBController(IMyProfileService arg)
        {
            service = arg;
        }

        async public Task<ActionResult> Index(FirstModel model)
        {

            if (model.UserId != null)
            {
                return View(model);
            }

            // using System.Web.Mvc
            Claim claim = (from result in HttpContext.GetOwinContext().Authentication.User.Claims
                           where result.Type == TokenHelper.FB_User_Id_Claim_Type
                           select result).First();

            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            // 承認時に得たクレームを出力。
            log.Info(string.Format("user name : {0} user id {1}", claim.Subject.Name, claim.Value));

            this.service.AccessToken = HttpContext.GetOwinContext().Authentication.User.Claims.Where(T => T.Type == TokenHelper.FB_Access_Token_Claim_Type).First().Value;
            this.service.UserId = claim.Value;

            FirstModel user = await this.service.GetMyProfile(null);
            ViewBag.Message = "This is index.";
            //FirstModel model = new FirstModel();
            //model.Alias = "webapp";

            if (user.Changed || !user.Exists)
            {
                return this.RedirectToAction("FBInfo", user);
            }

            if (user.BirthDay == null)
            {
                // 誕生日は非公開の時もあります。
                //return this.RedirectToAction("BirthDayNotAllowed", "Error");
            }

            user = this.service.GetMyProfileByUniqueKey(user);
            return View(user);
        }

        public ActionResult FBInfo(FirstModel fbInfo)
        {
            //log.Debug(this.Request.UrlReferrer.ToString());
            this.ViewBag.Title = "FaceBook の情報の登録";
            return this.View(fbInfo);
        }


        [HttpPost]
        public ActionResult Update(FirstModel model)
        {
            FirstModel result = ((MyProfileFBService)this.service).UpdateUserInfo(model);

            ActionResult ar = null;
            if (result.Changed)
            {
                model = this.service.GetMyProfileByUniqueKey(model);
                ar = this.RedirectToAction("Index", model);
            }
            else
            {
                //ar = this.RedirectToAction("MsUserNotFound", "Error", new { alias = model.Alias });
                ar = this.RedirectToAction("MsUserNotFound", "Error", model);
            }
            return ar;
        }

    }
}