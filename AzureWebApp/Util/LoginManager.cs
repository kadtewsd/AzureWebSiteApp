using AzureWebApp.Auth;
using AzureWebApp.Dao.PostLogin;
using AzureWebApp.Service.PostLogin;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace AzureWebApp.Util
{
    public class LoginManager
    {
        private static  AuthorizationType type;
        static LoginManager()
        {
            //type = AuthorizationType.FaceBook;
            type = AuthorizationType.OpenIdOnAzure;

            settings = new Dictionary<AuthorizationType, AbstractConfigurationSettings>()
            {
                { AuthorizationType.OpenIdOnAzure, new AzureConfigurationSettings()},
                { AuthorizationType.FaceBook, new FBConfigurationSettings()},
                { AuthorizationType.Twitter, new AzureConfigurationSettings()},
            };

            config = new Dictionary<AuthorizationType, AbstractAuthConfigure>()
            {
                { AuthorizationType.OpenIdOnAzure, new OpenIdAuth() },
                { AuthorizationType.FaceBook, new FBAuth()},
                { AuthorizationType.Twitter, new OpenIdAuth()},
            };
        }

        private static IDictionary<AuthorizationType, AbstractAuthConfigure> config = null;

        private static IDictionary<AuthorizationType, AbstractConfigurationSettings> settings = null;


        public enum AuthorizationType
        {
            OpenIdOnAzure,
            FaceBook,
            Twitter
        }

        public static AbstractAuthConfigure GetConfigure()
        {

            return config[type];
        }

        public static AbstractConfigurationSettings GetSettings()
        {
            return settings[type];
        }

        public static void ConfigureDependency(UnityContainer container)
        {
           switch (type)
            {
                case AuthorizationType.OpenIdOnAzure:
                case AuthorizationType.Twitter:

                    container.RegisterType<IMyProfileService, MyProfileAzureService>();
                    container.RegisterType<IMyProfileDao, MyProfileAzureDao>();
                    break;
                case AuthorizationType.FaceBook:
                    container.RegisterType<IMyProfileService, MyProfileFBService>();
                    container.RegisterType<IMyProfileDao, MyProfileFBDao>();
                    break;
            }
        }

    }
}