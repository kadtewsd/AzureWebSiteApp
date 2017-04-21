using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AzureWebApp.Model
{
    [DataContract]
    public class BucketModel
    {
        [DataMember()]
        public string Title { get; set; }
        [DataMember()]
        public string BucketId { get; set; }

        public IList<TaskModel> Tasks { get; set; }

    }
}
