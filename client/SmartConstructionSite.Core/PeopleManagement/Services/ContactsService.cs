using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.PeopleManagement.Models;
using SmartConstructionSite.Core.ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.PeopleManagement.Services
{
    public class ContactsService : ServiceBase
    {
        public async Task<Result<IList<User>>> Find(Project project, Department department = null, int page = 1, int pageSize = 10)
        {
            var result = new Result<IList<User>>();
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    var url = string.Format(Config.getUsersByProjIDUrl, project._id);
                    if (department != null)
                        url = string.Format(Config.getUsersByDeptIDUrl, project._id, department._id);
                    var msg = await httpClient.GetAsync(url);
                    var statJson = await msg.Content.ReadAsStringAsync();
                    var stat = JsonConvert.DeserializeObject<JObject>(statJson);
                    if ((bool)stat["success"])
                    {
                        var usersJson = stat["data"].ToString();
                        result.Model = JsonConvert.DeserializeObject<IList<User>>(usersJson);
                    }
                    else
                    {
                        result.HasError = true;
                        result.Error = new Error() { Description = (string)stat["msg"] };
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                result.HasError = true;
                result.Error = new Error() { Description = e.Message, Exception = e };
            }
            return result;
        }

        public async Task<Result<IList<Department>>> FetchDepartments(Project project, int page = 1, int pageSize = 10)
        {
            var result = new Result<IList<Department>>();
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    var msg = await httpClient.GetAsync(string.Format(Config.getDepartmentsUrl, project._id));
                    var statJson = await msg.Content.ReadAsStringAsync();
                    var stat = JsonConvert.DeserializeObject<JObject>(statJson);
                    if ((bool)stat["success"])
                    {
                        result.Model = JsonConvert.DeserializeObject<IList<Department>>(stat["data"].ToString());
                    }
                    else
                    {
                        result.HasError = true;
                        result.Error = new Error() { Description = stat["msg"].ToString() };
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                result.HasError = true;
                result.Error = new Error() { Description = e.Message, Exception = e };
            }
            return result;
        }
    }
}
