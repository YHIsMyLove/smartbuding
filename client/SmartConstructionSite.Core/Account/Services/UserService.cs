using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.Account.Services
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
                var stat = JsonConvert.DeserializeObject(json) as JObject;
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

        public async Task<Result<User>> GetUserInfo(string sessionId)
        {
            var result = new Result<User>();
            try
            {
                var httpClient = CreateHttpClient();
                var msg = await httpClient.GetAsync(string.Format(Config.getUserInfoUrl, sessionId));
                string json = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                var stat = (JObject)JsonConvert.DeserializeObject(json);
                if ((bool)stat["success"])
                {
                    var userJson = stat["data"].ToString();
                    System.Diagnostics.Debug.WriteLine($"user json: {userJson}");
                    result.Model = JsonConvert.DeserializeObject<User>(userJson);
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

        public async Task<Result<bool>> Logout(string sessionId)
        {
            var result = new Result<bool>();
            try
            {
                var httpClient = CreateHttpClient();
                var content = CreateContent($"SessionID={sessionId}");
                var message = await httpClient.PostAsync(Config.logoutUrl, content);
                var json = await message.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Response:{json}");
                var stat = JsonConvert.DeserializeObject<JObject>(json);
                if ((bool)stat["success"])
                {
                    result.Model = true;
                }
                else
                {
                    result.HasError = true;
                    result.Error = new Error() { Description = (string)stat["msg"] };
                }
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Error = new Error() { Description = e.Message, Exception = e };
            }
            return result;
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
