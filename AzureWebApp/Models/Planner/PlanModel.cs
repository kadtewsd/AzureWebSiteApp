using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AzureWebApp.Model
{
    [DataContract]
    public class PlanModel
    {
        [DataMember()]
        public string Title { get; set; }
        [DataMember()]
        public string PlanId { get; set; }
        [DataMember()]
        public string GroupName { get; set; }
        [DataMember()]
        public string Owner { get; set; }
        [DataMember()]
        public IList<BucketModel> BucketList { get; set; }
    }
}
