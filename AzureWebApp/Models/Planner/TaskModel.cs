using System.Runtime.Serialization;

namespace AzureWebApp.Model
{
    [DataContract]
    public class TaskModel
    {
        [DataMember()]
        public string Title { get; set; }
        [DataMember()]
        public string TaskId { get; set; }
    }
}
