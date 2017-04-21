using Facebook;

namespace AzureWebApp.Util
{
    public class JsonUtil
    {
        private JsonObject json = null;
        public JsonUtil (JsonObject argJson)
        {
            json = argJson;
        }

        public string GetValue(string key)
        {
            return json.ContainsKey(key) ? (string)json[key] : null;
        }
    }
}