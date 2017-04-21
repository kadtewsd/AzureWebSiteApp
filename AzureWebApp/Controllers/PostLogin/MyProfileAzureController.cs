using AzureWebApp.Models;
using AzureWebApp.Models.Model;
using AzureWebApp.Service.PostLogin;
using AzureWebApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AzureWebApp.Controllers.PostLogin
{
    //Authorize がないと OAuth のロジックは動きません。
    [Authorize]
    public class MyProfileAzureController : BaseController
    {

        IMyProfileService service = null;
        // コンストラクターを使うと、Bootstrap でオブジェクトが生成できない。
        public MyProfileAzureController(IMyProfileService arg)
        {
            service = arg;
        }


        async public Task<ActionResult> Index()
        {
            //FirstModel model = (FirstModel)fbInfo;
            BaseModel model = new FirstModel
            {
                RequestInfo = new Models.RequestInfo()
            };

            this.RequestHandler.SetRequestInfo(Request, model.RequestInfo);
            FirstModel result = await this.service.GetMyProfile(model);
            return this.View(result);
        }
    }
}