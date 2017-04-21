using AzureWebApp.Model;
using AzureWebApp.Request.Planner;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Web.Http;

namespace AzureWebApp.Controllers.Planner
{
    [Authorize]
    public class PlannerWebApiController  : BaseWebApi
    {
        private IPlansRequest<PlannerModel> service = null;

        [Dependency()]
        public IPlansRequest<PlannerModel> PlansRequest
        {
            get
            {
                return this.service == null ? new PlansRequest() : this.service;
            }
            set
            {
                this.service = value;
            }
        }

        public  async Task<PlannerModel> Get(PlannerModel model)
        {
            model = new PlannerModel();
            string taskresult =  await this.RequestHandler.SetAccessToken(model);
            PlannerModel result = PlansRequest.DoRequest(model);
            return result;
        }
    }
}