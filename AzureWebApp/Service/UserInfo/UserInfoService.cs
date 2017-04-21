using AzureWebApp.Dao.UserInfo;
using AzureWebApp.Models.UserInfo;
using Microsoft.Practices.Unity;

namespace AzureWebApp.Service.UserInfo
{
    public class UserInfoService : IUserInfoService
    {
        //private IUserInfoSelectDao dao = null;
        private IUserInfoDao dao = null;
        [Dependency]
        //public IUserInfoSelectDao UserInfoDao
        public IUserInfoDao UserInfoDao
        {
            set
            {
                this.dao = value;
            }
        }

        public UserInfoModel GetUserInfo(string alias)
        {
            //Tuple<string, string, string, string> integ = stub.StubDataq.GetIntegData(alias);
            //UserInfoModel model = new UserInfoModel();
            //model.DisplayName = integ.Item1;
            //model.Alias = alias;
            //model.EmailAddress = integ.Item3;
            //model.Hobby = integ.Item4;
            UserInfoModel model = new UserInfoModel();
            model.Alias = alias;
            //return dao.ExecuteSQL(model);
            return dao.GetUserInfo(model);
        }

        public bool InsertUserInfo(UserInfoModel userInfo)
        {
            return true;
        }

        public bool UpdateUserInfo(UserInfoModel userInfo)
        {
            return true;
        }
    }
}