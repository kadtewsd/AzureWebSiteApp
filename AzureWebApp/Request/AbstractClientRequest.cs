using AzureWebApp.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AzureWebApp.Request
{
    public abstract class AbstractClientRequest<T> where T : BaseModel
    {
        public T DoRequest(T form)
        {
            string ac = form.RequestInfo.AccessToken;
            T result = Activator.CreateInstance<T>();
            T transformed = null;
            using (HttpClient httpClient = new HttpClient())
            {
                using (var request = this.CreateHttpRequestMessage(form))
                {
                    request.Headers.Add("Authorization", string.Format("Bearer {0}", ac));

                    using (var response = httpClient.SendAsync(request).Result)
                    {
                        result.ResponseInfo.StatusCode = response.StatusCode;
                        result.ResponseInfo.Content =  response.Content.ReadAsAsync(typeof(string)).Result.ToString();
                        if (response.IsSuccessStatusCode)
                        {
                            string JSON = response.Content.ReadAsStringAsync().Result;
                            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                            byte[] bytes = null;
                            try
                            {
                                bytes = Encoding.UTF8.GetBytes(result.ResponseInfo.JSON);
                                MemoryStream ms = new MemoryStream(bytes);
                                result = (T)serializer.ReadObject(ms);
                            }
                            catch
                            {
                                Console.Write("request is failed...");
                            }
                            result = this.Execute(result);
                            result.ResponseInfo.JSON = JSON;
                        }
                    }
                }
            }
            return transformed;
        }

        protected abstract T Execute(T model);

        protected abstract HttpRequestMessage CreateHttpRequestMessage(T form);

    }
}