using AzureWebApp.Models.UserInfo;

namespace AzureWebApp.Dao.UserInfo
{
    public class UserInfoDao : BaseSqlMapDao, IUserInfoDao
    {
        public UserInfoModel GetUserInfo(UserInfoModel userInfo)
        {
            return this.ExecuteQueryForObject<UserInfoModel>("selectTargetUserInfo", userInfo);
        }
    }
}
