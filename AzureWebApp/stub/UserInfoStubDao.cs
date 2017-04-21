using AzureWebApp.Dao.UserInfo;
using AzureWebApp.Models.UserInfo;
using System;
using System.Collections.Generic;

namespace AzureWebApp.stub
{
    public class UserInfoStubDao : IUserInfoSelectDao
    {
        private static IDictionary<string, Tuple<string, string, string, string>> stub = null;

        static UserInfoStubDao() {

            stub = new Dictionary<string, Tuple<string, string, string, string>>
            {
                {"webapp", new Tuple<string, string, string, string>("境田一輝", "", "kXXXXXd@hogehoge.com", "寝る" ) },
                {"nofukuyo", new Tuple<string, string, string, string>("福世信隆", "", "nXXXXXo@hogehoge.com", "ぶらんど" )},
                { "sasaeki", new Tuple<string, string, string, string>("佐伯さとみ", "", "sXXXXXki@hogehoge.com", "りょうり" )},
                { "kenmori", new Tuple<string, string, string, string>("森けんご", "", "kXXXXXi@microsoft.com", "ぎゃぐ")},
                { "makiyama", new Tuple<string, string, string, string>("秋山さん", "", "mXXXXXa@hogehoge.com", "子育て" )},
                { "yoshish", new Tuple<string, string, string, string>("島崎さん", "", "yXXXXXh@hogehoge.com", "たばこ" )}
            };
        }

        private static IDictionary<string, Tuple<string, string, string, string>> IntegData
        {
            get
            {
                return stub;
            }
        }

        public static Tuple<string, string, string, string> GetIntegData(string alias)
        {
            return stub[alias];
        }

        public UserInfoModel ExecuteSQL(UserInfoModel input)
        {
            return this.GetUserInfo(input.Alias);
        }

        public UserInfoModel GetUserInfo(string alias)
        {
            Tuple<string, string, string, string> result = GetIntegData(alias);
            UserInfoModel model = new UserInfoModel();
            model.Alias = alias;
            model.FamilyName = result.Item1;
            model.Email = result.Item3;
            model.Hobby = result.Item4;

            return model;

        }
    }
}