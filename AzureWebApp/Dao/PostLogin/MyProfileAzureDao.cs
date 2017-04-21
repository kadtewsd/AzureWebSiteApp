using System.Threading.Tasks;
using AzureWebApp.Util;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System.Linq;
using System;
using AzureWebApp.Models.Model;
using System.Net.Http;
using AzureWebApp.Models;

namespace AzureWebApp.Dao.PostLogin
{
    public class MyProfileAzureDao : IMyProfileDao
    {
        public async Task<object> GetMyProfile(BaseModel baseModel)
        {
            Util.TokenHelper helper = new Util.TokenHelper();

            AbstractConfigurationSettings settings = LoginManager.GetSettings();

            Uri servicePointUri = new Uri(settings.GraphResourceID);
            Uri serviceRoot = new Uri(servicePointUri, settings.TenantID);

            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                  async () => await helper.GetTokenForApplication());
            //dynamic data = null;
            //using (HttpClient client = new HttpClient())
            //{
            //    //https://graph.microsoft.com/v1.0/c8bcc4f2-e141-42b4-9c97-4eec36ad4896/users/10857d1b-0fb0-4dc9-abe0-99fc84fd8133
            //    Uri usersEndpoint = new Uri(servicePointUri, "v1.0/" + settings.TenantID + "/users/" + settings.UserObjectID);
            //    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, usersEndpoint);
            //    string accessToken = await helper.GetTokenForApplication();
            //    requestMessage.Headers.Add(
            //            System.Net.HttpRequestHeader.Authorization.ToString(),
            //            string.Format("Bearer {0}", accessToken));

            //    requestMessage.Headers.Add(System.Net.HttpRequestHeader.Accept.ToString(), "application/json, text/plain, */*");
            //    requestMessage.Headers.Add(System.Net.HttpRequestHeader.UserAgent.ToString(), baseModel.RequestInfo.UserAgent);
            //    requestMessage.Headers.Add(System.Net.HttpRequestHeader.AcceptLanguage.ToString(), "Accept-Language: ja,en-US;q=0.8,en;q=0.6");
            //    requestMessage.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding.ToString(), "gzip, deflate, sdch, br");


            //    using (var httpResponseMessage = await client.SendAsync(requestMessage))
            //    {
            //        // do something with the response
            //        data = requestMessage.Content;
            //    }
            //} 
            //IUser user = new User();
            //user.DisplayName = data.DisplayName;
            //user.Mail = data.Mail;
            //user.GivenName = data.GivenName;
            //user.JobTitle = data.JobTitle;
            //user.UserPrincipalName = data.UserPrincipalName;

            // use the token for querying the graph to get the user details
            //https:graph.microsoft.com/c8bcc4f2-e141-42b4-9c97-4eec36ad4896/users/10857d1b-0fb0-4dc9-abe0-99fc84fd8133?api-version=1.6
            //var result = await activeDirectoryClient.Me.ExecuteAsync();
            // このリクエストは、https://graph.windows.net だとうまくいく。前はgraph.microsoft.com でうまくいっていたのに。。。
            var result = await activeDirectoryClient.Users
                .Where(aduser => aduser.ObjectId.Equals(settings.UserObjectID))
                .ExecuteAsync();
                //.ExecuteSingleAsync();

            IUser user = result.CurrentPage.ToList().First();
            //IUser user = result;

            return user;
        }

        public FirstModel GetMyProfileByUniqueKey(FirstModel model)
        {
            throw new NotImplementedException();
        }

        public void UpsertUserInfo(FirstModel mode)
        {
            throw new NotImplementedException();
        }

        public int InsertUserInfo(FirstModel model)
        {
            throw new NotImplementedException();
        }

        public int UpdateUserInfo(FirstModel model)
        {
            throw new NotImplementedException();
        }

        public string AccessToken
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }
    }
}
