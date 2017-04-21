using System;
using AzureWebApp.Dao.PostLogin;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AzureWebApp.Models.Model;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using AzureWebApp.Models;

namespace AzureWebApp.Service.PostLogin
{
    public class MyProfileAzureService : IMyProfileService
    {
        private IMyProfileDao dao = null;

        [InjectionConstructor]
        public MyProfileAzureService (IMyProfileDao arg)
        {
            dao = arg;
        }

        public string AccessToken
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }

        async public Task<FirstModel> GetMyProfile(BaseModel baseModel)
        {
            FirstModel result = new FirstModel();

            try
            {
                result.User = await dao.GetMyProfile(baseModel) as IUser;
            }
            catch (AdalException e)
            {
                result.Message = "Adal Error";
                result.Exception = e;

            }
            catch (Exception e)
            {
                result.Message = "Relogin";
                result.Exception = e;
            }
            return result;
        }

        /// <summary>
        /// get my profile by MS Alias.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FirstModel GetMyProfileByUniqueKey(FirstModel model)
        {
            throw new NotImplementedException();
        }
    }
}