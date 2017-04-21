using System.Collections.Generic;

namespace AzureWebApp.Models.UserInfo
{
    public class UserInfoModel : BaseModel
    {

        public override string DisplayName
        {
            get
            {
                return this.displayName != null ? this.displayName : FamilyName + " " + FirstName;
            }
            set
            {
                this.displayName = value;
            }
        }

        public string Hobby { get; set; }

        public string Supplier { get; set; }

        public IList<UserRumor> Rumors { get; set; }

    }
}