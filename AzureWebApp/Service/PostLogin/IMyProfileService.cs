using AzureWebApp.Models;
using AzureWebApp.Models.Model;
using System.Threading.Tasks;

namespace AzureWebApp.Service.PostLogin
{
    public interface IMyProfileService
    {
        Task<FirstModel> GetMyProfile(BaseModel baseModel);

        FirstModel GetMyProfileByUniqueKey(FirstModel model);

        string AccessToken { get; set; }

        string UserId { get; set; }
    }
}
