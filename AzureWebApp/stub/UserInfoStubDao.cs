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
                {"webapp", new Tuple<string, string, string, string>("a", "", "kXXXXXd@hogehoge.com", "寝る" ) },
                {"aaaa", new Tuple<string, string, string, string>("b", "", "nXXXXXo@hogehoge.com", "ぶらんど" )},
                { "bbbbb", new Tuple<string, string, string, string>("c", "", "sXXXXXki@hogehoge.com", "りょうり" )},
                { "xxxxxi", new Tuple<string, string, string, string>("d", "", "kXXXXXi@hoge.com", "ぎゃぐ")},
                { "ddgggg", new Tuple<string, string, string, string>("e", "", "mXXXXXa@hogehoge.com", "子育て" )},
                { "3eesss", new Tuple<string, string, string, string>("f", "", "yXXXXXh@hogehoge.com", "たばこ" )}
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