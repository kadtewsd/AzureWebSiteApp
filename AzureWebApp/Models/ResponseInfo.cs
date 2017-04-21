using System.Net;

namespace AzureWebApp.Models
{
    public class ResponseInfo
    {
        public HttpStatusCode StatusCode { get; set; }

        public  string JSON { get; set; }

        public string Content { get; set; }
    }
}