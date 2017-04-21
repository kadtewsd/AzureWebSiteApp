using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using System.Reflection;
using System.Linq;
using System.Data;
using AzureWebApp.stub;
using AzureWebApp.Service.UserInfo;
using AzureWebApp.Dao.UserInfo;
using AzureWebApp.Util;
using System.Text.RegularExpressions;

namespace AzureWebApp
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            

            //container.RegisterType<IUserInfoService, UserInfoService>();
            //container.RegisterType<IController, UserInfoController>("Store");
            const string TypePattern = "[`[0-9]*]*";
            string assemblyName = Assembly.GetAssembly(new DebugUtil().GetType()).GetName().Name;
            var results = Assembly.Load(assemblyName)
               .GetTypes()
               .Where(Type => Type.GetInterfaces().Any(
                    Interface => new Regex("I" + Type.Name + TypePattern).IsMatch(Interface.Name))
               ).ToList();

            //T å^ÇégÇ¡ÇΩ IPlansRequest`1 ÇÃëŒçÙ
            foreach (var result in results)
            {
                foreach (var iFace in result.GetInterfaces())
                {
                    if (new Regex("I" + result.Name + TypePattern).IsMatch(iFace.Name))
                    {
                        container.RegisterType(iFace, result);
                        break;
                    }
                }
            }

            //Assembly.Load(assemblyName)
            //   .GetTypes()
            //   .Where(Type => Type.GetInterfaces().Any(
            //        Interface => new Regex("I" + Type.Name + "[`[0-9]]*").IsMatch(Interface.Name))
            //   ).ToList().ForEach(Type => container.RegisterType(Type.GetInterfaces()[0], Type));
            // T å^ÇégÇ¡ÇΩ IPlansRequest`1 ÇÃëŒçÙ


            if (DebugUtil.IsDebug())
            {
                container.RegisterType<IUserInfoService, UserInfoService>();
                container.RegisterType<IUserInfoSelectDao, UserInfoStubDao>();
            }

            LoginManager.ConfigureDependency(container);

            return container;
        }
    }
}