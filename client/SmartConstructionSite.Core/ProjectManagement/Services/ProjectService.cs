using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.ProjectManagement.Services
{
    public class ProjectService : ServiceBase
    {
        public ProjectService()
        {
            
        }

        internal async Task<Result<IList<Project>>> FindProjects(Province province = null, City city = null)
        {
            var result = new Result<IList<Project>>();
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    string url = string.Format(Config.getProjsByUser, ServiceContext.Instance.CurrentUser._id);
                    if (province != null)
                        url = string.Format(Config.getProjectsByProvIDUrl, province._id);
                    else if (city != null)
                        url = string.Format(Config.getProjectsByCityIDUrl, city._id);
                    var msg = await httpClient.GetAsync(url);
                    string json = await msg.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                    var stat = JsonConvert.DeserializeObject<JObject>(json);
                    if ((bool)stat["success"])
                    {
                        string provsJson = stat["data"].ToString();
                        result.Model = JsonConvert.DeserializeObject<IList<Project>>(provsJson);
                    }
                    else
                    {
                        result.HasError = true;
                        result.Error = new Error() { Description = (string)stat["msg"], Code = 1001 };
                    }
                }
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Error = new Error() { Description = e.Message, Exception = e, Code = 1000 };
            }
            return result;
        }

        internal async Task<Result<IList<Province>>> FetchProvinces()
        {
            var result = new Result<IList<Province>>();
            try
            {
                var httpClient = CreateHttpClient();
                var msg = await httpClient.GetAsync(string.Format(Config.getProvByUserUrl, ServiceContext.Instance.CurrentUser._id));
                string json = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                var stat = JsonConvert.DeserializeObject<JObject>(json);
                if ((bool)stat["success"])
                {
                    string provsJson = stat["data"].ToString();
                    result.Model = JsonConvert.DeserializeObject<IList<Province>>(provsJson);
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

        internal async Task<Result<IList<City>>> FetchCities(Province province)
        {
            var result = new Result<IList<City>>();
            try
            {
                var httpClient = CreateHttpClient();
                var msg = await httpClient.GetAsync(string.Format(Config.getCitiesUrl, province._id));
                string json = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response:{0}", json);
                var stat = JsonConvert.DeserializeObject<JObject>(json);
                if ((bool)stat["success"])
                {
                    string citysJson = stat["data"].ToString();
                    result.Model = JsonConvert.DeserializeObject<IList<City>>(citysJson);
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
