using AzureWebApp.Models.UserInfo;

namespace AzureWebApp.Service.UserInfo
{
    public interface IUserInfoService
    {
        UserInfoModel GetUserInfo(string alias);

        bool InsertUserInfo(UserInfoModel userInfo);

        bool UpdateUserInfo(UserInfoModel userInfo);

    }
}
