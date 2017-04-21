using AzureWebApp.Models.UserInfo;

namespace AzureWebApp.Dao.UserInfo
{
    public interface IUserInfoDao
    {
        UserInfoModel GetUserInfo(UserInfoModel userInfo);
    }
}
