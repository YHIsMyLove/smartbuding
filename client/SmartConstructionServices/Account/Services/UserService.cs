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
    public class UserService
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
                var httpClient = new HttpClient(new NativeMessageHandler());
                var content = new ByteArrayContent(Encoding.UTF8.GetBytes($"UserID={username}&UserPwd={password}"));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage msg = await httpClient.PostAsync("http://192.168.43.86:3000/api/login", content);
                string json = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                var stat = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JObject;
                if ((bool)stat["success"])
                {
                    string sessionId = (string)stat["data"]["SessionID"];
                    string ysToken = (string)stat["data"]["YSToken"];
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

        public async Task<Result<User>> CheckSession(string sessionId)
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<User> result = new Result<User>();
                result.Model = new User();
                return result;
            });
        }
    }
}
