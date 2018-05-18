using System;
using System.Text;

namespace SmartConstructionSite.Core.Common
{
    public class ServiceBase
    {
        public ServiceBase()
        {
        }

        public System.Net.Http.HttpClient CreateHttpClient()
        {
            return new System.Net.Http.HttpClient(new ModernHttpClient.NativeMessageHandler())
            {
                Timeout = new TimeSpan(0, 0, 10)
            };
        }

        public System.Net.Http.HttpContent CreateContent(string parameters)
        {
            var content = new System.Net.Http.ByteArrayContent(Encoding.UTF8.GetBytes(parameters));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            return content;
        }
    }
}
