using AzureWebApp.Models;
using AzureWebApp.Models.Model;
using System.Threading.Tasks;

namespace AzureWebApp.Dao.PostLogin
{
    public interface IMyProfileDao
    {
        Task<object> GetMyProfile(BaseModel baseModel);

        FirstModel GetMyProfileByUniqueKey(FirstModel model);

        string AccessToken { get; set; }

        string UserId { get; set; }

        void UpsertUserInfo(FirstModel mode);

        int InsertUserInfo(FirstModel model);

        int UpdateUserInfo(FirstModel model);
    }
}
