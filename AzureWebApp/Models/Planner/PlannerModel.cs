using AzureWebApp.Models;
using System.Collections.Generic;
using System;

namespace AzureWebApp.Model
{
    public class PlannerModel : BaseModel
    {

        public IList<PlanModel> Plans;

        public override string DisplayName
        {
            get
            {
                return displayName;
            }

            set
            {
                displayName = value;
            }
        }
    }
}
