using AzureWebApp.Models.UserInfo;
using System.Data.SqlClient;

namespace AzureWebApp.Dao.UserInfo
{
    public class UserInfoSelectDao : SQLBaseDao<UserInfoModel, UserInfoModel>, IUserInfoSelectDao
    {
        public UserInfoSelectDao() : base()
        {
            ResultValue = new UserInfoModel();
        }


        private static string SQL = @"
            select 
				a.alias, 
				a.familyName,
				a.firstName,
				a.emailAddress, 
				b.hobby,
				(select
					familyName + ' ' + firstName
					from
						USER_INFO c
					where b.supplier = c.alias
				) as supplier
			from USER_INFO a
            	inner join HOBBY_INFO b
		            on a.alias = b.alias
            where a.alias = @alias

";

        protected override UserInfoModel Execute(UserInfoModel input)
        {
            return this.GetUserInfo(input.Alias);
        }

        public UserInfoModel GetUserInfo(string alias)
        {
            SqlParameter param = new SqlParameter("@alias", alias);
            this.Command.CommandText = SQL;
            this.Command.Parameters.Add(param);
            return this.Select();
        }

        protected override  UserInfoModel ResultValue { get; set; }

    }
}