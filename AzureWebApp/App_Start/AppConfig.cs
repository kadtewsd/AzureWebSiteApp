using log4net.Config;
using System;
using System.IO;

namespace AzureWebApp.App_Start
{
    public class AppConfig
    {
        public static void Configure()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log4net.config"));
        }
    }
}