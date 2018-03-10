using ModernHttpClient;
using SmartConstructionServices.Account.Models;
using SmartConstructionServices.Common;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Account.Services
{
    public class UserService
    {
        public async Task<Result<User>> Login(string username, string password)
        {
            Result<User> result = new Result<User>();
            string param = string.Format("UserID={0}&UserPwd={1}", username, password);
            byte[] bs = Encoding.UTF8.GetBytes(param);
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());
                var content = new ByteArrayContent(Encoding.UTF8.GetBytes($"UserID={username}&UserPwd={password}"));
                HttpResponseMessage msg = await httpClient.PostAsync("http://192.168.1.49:3000/api/login", content);
                string json = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                dynamic jsonObj = Newtonsoft.Json.Linq.JToken.Parse(json) as dynamic;
                if (jsonObj.Success)
                {

                }
            }
            catch (Exception e)
            {
                
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
