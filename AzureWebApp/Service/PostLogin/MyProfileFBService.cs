using System;
using AzureWebApp.Dao.PostLogin;
using AzureWebApp.Models.Model;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Facebook;
using AzureWebApp.Util;
using AzureWebApp.Models;

namespace AzureWebApp.Service.PostLogin
{
    public class MyProfileFBService : IMyProfileService
    {
        private IMyProfileDao dao = null;

		[InjectionConstructor]
        public MyProfileFBService(IMyProfileDao arg)
        {
            dao = arg;
        }

        async public Task<FirstModel> GetMyProfile(BaseModel baseModel)
        {
            FirstModel result = new FirstModel();

            try
            {
                this.dao.AccessToken = this.AccessToken;
                this.dao.UserId = this.UserId;
                // Get as JSON
                JsonObject json = (JsonObject) await dao.GetMyProfile(baseModel);
                JsonUtil util = new JsonUtil(json);

                result.DisplayName = util.GetValue("name");
                result.BirthDay = util.GetValue("birthday");
                result.Email = util.GetValue("email");
                result.UserId = util.GetValue("id");
                result.RowCount = this.dao.InsertUserInfo(result);
                result.Exists = false;
                if (!result.Changed)
                {
                    FirstModel dbInfo = this.dao.GetMyProfileByUniqueKey(result);
                    result.Exists = dbInfo != null;
                    result.Email = dbInfo.Email;
                }
                
                //this.fbDao.UpsertUserInfo(result);
            }
            catch (Facebook.FacebookOAuthException ex)
            {
                // oauth exception occurred
                result.Message = "FacebookOAuthException";
                result.Exception = ex;
            }
            catch (Facebook.FacebookApiLimitException ex)
            {
                result.Message = "FacebookApiLimitException";
                result.Exception = ex;
            }
            catch (Facebook.FacebookApiException ex)
            {
                result.Message = "FacebookApiException";
                result.Exception = ex;
            }
            catch (Exception e)
            {
                result.Message = "Relogin";
                result.Exception = e;
            }
            return result;
        }

        public FirstModel GetMyProfileByUniqueKey(FirstModel model)
        {
            FirstModel result = this.dao.GetMyProfileByUniqueKey(model);
            model.Email = result.Email;
            return model;
        }

        public FirstModel UpdateUserInfo(FirstModel model)
        {
            int result = this.dao.UpdateUserInfo(model);
            model.RowCount = result;
            return model;
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