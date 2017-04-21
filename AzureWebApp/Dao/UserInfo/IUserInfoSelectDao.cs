using AzureWebApp.Models.UserInfo;

namespace AzureWebApp.Dao.UserInfo
{
    public interface IUserInfoSelectDao : IBaseDao<UserInfoModel, UserInfoModel>
    {
        UserInfoModel GetUserInfo(string alias);
    }
}
