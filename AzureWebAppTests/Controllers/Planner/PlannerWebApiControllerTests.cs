using AzureWebApp.Model;
using AzureWebApp.Models;
using webappWebAppTests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;

namespace AzureWebApp.Controllers.Planner.Tests
{
    [TestClass()]
    public class PlannerWebApiControllerTests  : BaseApiTestFixture
    {
        protected override string RequestUri
        {
            get
            {
                return "api/PlannerWebApi";
            }
        }

        [TestMethod()]
        public void GetTesrtt()
        {

            PlannerModel model = new PlannerModel
            {
                RequestInfo = new RequestInfo()
            };

            // This workaround is necessary on Xamarin,
            //which doesn't support async unit test methods.
            Task.Run(async () =>
            {
                string result = await this.DoTest();
                // Actrual test code here.
                //Inrstall-Package Microsoft.AspNet.WebApi
                Assert.AreSame(1, 1);
            }).GetAwaiter().GetResult();


    }
    }
}