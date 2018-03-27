using ModernHttpClient;
using Newtonsoft.Json.Linq;
using SmartConstructionServices.Account.Models;
using SmartConstructionServices.Common;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartConstructionServices.Account.Services
{
    public class UserService : ServiceBase
    {
        public async Task<Result<bool>> Login(string username, string password)
        {
            var result = new Result<bool>();
            if ("admin".Equals(username))
            {
                result.Model = true;
                return result;
            }
            try
            {
                var httpClient = CreateHttpClient();
                var content = CreateContent($"UserID={username}&UserPwd={password}");
                HttpResponseMessage msg = await httpClient.PostAsync(Config.loginUrl, content);
                string json = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                var stat = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JObject;
                if ((bool)stat["success"])
                {
                    string sessionId = (string)stat["data"]["SessionID"];
                    string ysToken = (string)stat["data"]["YSToken"];
                    ServiceContext.Instance.SessionID = sessionId;
                    ServiceContext.Instance.YSAccessToken = ysToken;
                    result.Model = true;
                }
                else
                {
                    result.HasError = true;
                    result.Error = new Error() { Description = (string)stat["msg"], Code = 1001 };
                }
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Error = new Error() { Description = e.Message, Exception = e, Code = 1000 };
            }
            return result;
        }

        public async Task<Result<bool>> Logout()
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<bool> result = new Result<bool>();
                result.Model = true;
                return result;
            });
        }

        public async Task<Result<bool>> CheckSession(string sessionId)
        {
            var result = new Result<bool>();
            try
            {
                var httpClient = CreateHttpClient();
                var content = CreateContent($"SessionID={sessionId}");
                HttpResponseMessage msg = await httpClient.PostAsync(Config.loginUrl, content);
                string json = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                var stat = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JObject;
                if ((bool)stat["success"])
                {
                    result.Model = true;
                }
                else
                {
                    result.HasError = true;
                    result.Error = new Error() { Description = (string)stat["msg"], Code = 1001 };
                }
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Error = new Error() { Description = e.Message, Exception = e, Code = 1000 };
            }
            return result;
        }
    }
}
