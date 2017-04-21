using AzureWebApp.Models.UserInfo;
using AzureWebApp.Service.Message;
using AzureWebApp.Service.UserInfo;
using Microsoft.Practices.Unity;

namespace AzureWebApp.Pages.UserInfo
{
    public class UserInfoPage : BasePage<UserInfoModel>
    {

        private IUserInfoService service;
        public UserInfoPage(IUserInfoService service)
        {
            this.service = service;
        }

        [Dependency]
        public IMessageService MessageService { get; set; }

        public override void Execute()
        {
            //this.
            //UserInfoService.GetUserInfo()
        }
    }
}