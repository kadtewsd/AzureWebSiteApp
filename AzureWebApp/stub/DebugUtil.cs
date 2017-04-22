using System.Web;

namespace AzureWebApp.stub
{
    public class DebugUtil
    {
        public static bool IsDebug()
        {
            try
            {
                string url = HttpRuntime.AppDomainAppPath;
                return (url.Contains("AzureWebApp\\AzureWebApp"));
                //return Environment.UserDomainName.ToLower().Contains("webapp") || Environment.UserDomainName.ToLower().Contains("fareast");
            }
            //catch (Exception e)
            //{
            //    throw e;
            //    //return false;
            //}
            catch
            {
                // Test クラスから実行された時は、URL はありません。
                return true;
            }
        }
    }
}