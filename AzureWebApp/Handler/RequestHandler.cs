using AzureWebApp.Models;
using AzureWebApp.Util;
using System.Threading.Tasks;

namespace AzureWebApp.Handler
{
    public class RequestHandler
    {

        private static TokenHelper helper = new TokenHelper();

        public void SetRequestInfo(System.Web.HttpRequestBase request, RequestInfo requestInfo)
        {
            requestInfo.UserAgent = request.UserAgent;
        }

        public async Task<string> SetAccessToken(BaseModel model)
        {
            model.RequestInfo = model.RequestInfo == null ? new RequestInfo() : model.RequestInfo;
            model.RequestInfo.AccessToken = await helper.GetTokenForApplication();
            return model.RequestInfo.AccessToken;
        }
    }
}
