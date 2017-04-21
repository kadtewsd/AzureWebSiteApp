using AzureWebApp.Models;

namespace AzureWebApp.Request.Planner
{
    public interface IPlansRequest<PlannerModel>
    {
        PlannerModel DoRequest(PlannerModel form);
    }
}
