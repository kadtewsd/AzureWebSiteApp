using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace AzureWebApp.Models.Model
{
    public class FirstModel : BaseModel
    {
        public IUser User { get; set; }

        public string SurName
        {
            get; set;
        }

        public string GivenName
        {
            get; set;
        }
        
        public override string DisplayName
        {
            get
            {
                return this.User != null ? User.Surname + " " + User.GivenName : displayName; 
            }
            set
            {
                this.displayName = value;
            }
        }

        public bool Exists { get; set; }

    }
}