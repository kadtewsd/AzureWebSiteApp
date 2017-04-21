using AzureWebApp.Model;
using System.Net.Http;

namespace AzureWebApp.Request.Planner
{
    public class PlansRequest : AbstractClientRequest<PlannerModel>, IPlansRequest<PlannerModel>
    {
        protected override HttpRequestMessage CreateHttpRequestMessage(PlannerModel form)
        {
            return new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/beta/plans");
        }

        protected override PlannerModel Execute(PlannerModel model)
        {
            return model;
        }
    }
}