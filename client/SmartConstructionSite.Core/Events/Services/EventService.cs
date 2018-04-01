using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Events.Services
{
    public class EventService : ServiceBase
    {
        public async Task<Result<IList<PageTypeGroup>>> FetchLatestEvent()
        {
            var result = new Result<IList<PageTypeGroup>>();
            try
            {
                var httpClient = CreateHttpClient();
                var msg = await httpClient.GetAsync(Config.getMeetingsUrl);
                var statJson = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Response: {statJson}");
                var stat = JsonConvert.DeserializeObject<JObject>(statJson);
                if ((bool)stat["success"])
                {
                    var meetingsJson = stat["data"].ToString();
                    var meetings = JsonConvert.DeserializeObject<IList<Meeting>>(meetingsJson);
                    result.Model = meetings
                        .GroupBy(i => i.MettingCreatedAt.ToString("yyyy年MM月"))
                        .Select(i => {
                            var group = new PageTypeGroup(i.Key, "");
                            group.AddRange(i);
                            return group;
                        }).ToList();
                }
                else
                {
                    result.HasError = true;
                    result.Error = new Error() { Description = stat["msg"].ToString() };
                }
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Error = new Error() { Description = e.Message, Exception = e };
            }
            return result;
        }

        public Task<Result<IList<MeetingMinutes>>> FetchMeetingMinutes(Meeting meeting, int page = 1, int pageSize = 10)
        {
            return Task.Run(() =>
            {
                var result = new Result<IList<MeetingMinutes>>();
                result.Model = SimpleData.Instance.GetMeetingMinutes();
                return result;
            });
        }
    }
}
