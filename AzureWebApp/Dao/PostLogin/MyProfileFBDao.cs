using System.Threading.Tasks;
using Facebook;
using AzureWebApp.Util;
using System.Net;
using System.IO;
using System;
using AzureWebApp.Models.Model;
using AzureWebApp.Models;

namespace AzureWebApp.Dao.PostLogin
{
    //Microsoft.AspNet.Mvc.Facebook ではエラー
    // Install-Package Facebook -Version 7.0.6
    public class MyProfileFBDao : BaseSqlMapDao, IMyProfileDao
    {

        private static AbstractConfigurationSettings settings = LoginManager.GetSettings();

        public async Task<object> GetMyProfile(BaseModel baseModel)
        {
            //http://blog.takady.net/blog/2014/12/29/facebook-graph-api-oauth-flow/
            FacebookClient client = new FacebookClient();
            client.AppId = settings.ClientId;
            client.AppSecret = settings.ClientSecret;


            // Access Token を延長。
            JsonObject newAccessToken = (JsonObject) client.Get("oauth/access_token", new
            {
                client_id = client.AppId,
                client_secret = client.AppSecret,
                grant_type = "fb_exchange_token",
                fb_exchange_token = this.AccessToken,

            });

            //An active access token must be used to query information about the current user.
            dynamic fbAccounts = client.Get("v2.6/" + this.UserId, new
            {
                client_id = client.AppId,
                client_secret = client.AppSecret,
                access_token = newAccessToken["access_token"],
                fields = "name,birthday,email",
            });
            return fbAccounts;
        }

        [Obsolete]
        private void TestRequest()
        {
            FacebookClient client = new FacebookClient();
            client.AppId = settings.ClientId;
            client.AppSecret = settings.ClientSecret;

            string url = string.Format("https://www.facebook.com/oauth/authorize?client_id={0}&client_secret{1}&redirect_uri{2}", client.AppId, client.AppSecret, settings.AppRedirectUri);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";

            string authorizationCode = System.Web.HttpContext.Current.Request.QueryString["code"];

            StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream());
            // login 画面が返ってくる
            string responseData = responseReader.ReadToEnd();
        }

        public string AccessToken
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }

        public FirstModel GetMyProfileByUniqueKey(FirstModel model)
        {
            return this.ExecuteQueryForObject<FirstModel>("selectUserInfoByFBID", model);
        }

        public void UpsertUserInfo(FirstModel model)
        {
            this.ExecuteInsert("upsertFBInfo", model);
        }

        public int InsertUserInfo(FirstModel model)
        {
            return this.ExecuteInsert("insertFBInfo", model);
        }

        public int UpdateUserInfo(FirstModel model)
        {
            return this.ExecuteUpdate("UpdateFBInfo", model);
        }
    }
}
