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
        
        public async Task<Result<IList<Meeting>>> FetchLatestEvent()
        {
            var result = new Result<IList<Meeting>>();
            try
            {
                var httpClient = CreateHttpClient();
                var msg = await httpClient.GetAsync(string.Format(Config.getMeetingsUrl, ServiceContext.Instance.CurrentUser._id, ServiceContext.Instance.CurrentProject._id));
                var statJson = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"url: {Config.getMeetingsUrl} Response: {statJson}");
                var stat = JsonConvert.DeserializeObject<JObject>(statJson);
                if ((bool)stat["success"])
                {
                    var meetingsJson = stat["data"].ToString();
                    var meetings = JsonConvert.DeserializeObject<IList<Meeting>>(meetingsJson);
                    var list = meetings.ToList();
                    list.Sort((x, y) =>
                    {
                        if (x.MeetingCreatedAt > y.MeetingCreatedAt)
                            return -1;
                        else if (x.MeetingCreatedAt == y.MeetingCreatedAt)
                            return 0;
                        else
                            return 1;
                    });
                    result.Model = list;
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

        public async Task<Result<IList<Meeting>>> FetchMeetings(int year = -1, int month = -1, int day = -1)
        {
            var result = new Result<IList<Meeting>>();
            try
            {
                var httpClient = CreateHttpClient();
                var msg = await httpClient.GetAsync(string.Format(Config.getMeetingsUrl, ServiceContext.Instance.CurrentUser._id, ServiceContext.Instance.CurrentProject._id));
                var statJson = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"url: {Config.getMeetingsUrl} Response: {statJson}");
                var stat = JsonConvert.DeserializeObject<JObject>(statJson);
                if ((bool)stat["success"])
                {
                    var meetingsJson = stat["data"].ToString();
                    var meetings = JsonConvert.DeserializeObject<IList<Meeting>>(meetingsJson);
                    var list = meetings.Where((meeting) =>
                    {
                        if (day != -1)
                        {
                            return meeting.MeetingCreatedAt.Year == year
                                          && meeting.MeetingCreatedAt.Month == month
                                          && meeting.MeetingCreatedAt.Day == day;
                        }
                        else if (month != -1)
                        {
                            return meeting.MeetingCreatedAt.Year == year
                                          && meeting.MeetingCreatedAt.Month == month;
                        }
                        else if (year != -1)
                        {
                            return meeting.MeetingCreatedAt.Year == year;
                        }
                        return true;
                    }).ToList();
                    list.Sort((x, y) =>
                    {
                        if (x.MeetingCreatedAt > y.MeetingCreatedAt)
                            return -1;
                        else if (x.MeetingCreatedAt == y.MeetingCreatedAt)
                            return 0;
                        else
                            return 1;
                    });
                    result.Model = list;
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

        public async Task<Result<IList<MeetingMinutes>>> FetchMeetingMinutes(Meeting meeting, int page = 1, int pageSize = 10)
        {
            var result = new Result<IList<MeetingMinutes>>();
            try
            {
                var httpClient = CreateHttpClient();
                var msg = await httpClient.GetAsync(string.Format(Config.getMeetingMinutesUrl, ServiceContext.Instance.CurrentUser._id, meeting._id));
                var statJson = await msg.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"url: {Config.getMeetingMinutesUrl} Response: {statJson}");
                var stat = JsonConvert.DeserializeObject<JObject>(statJson);
                if ((bool)stat["success"])
                {
                    var meetingMinutesJson = stat["data"].ToString();
                    result.Model = JsonConvert.DeserializeObject<IList<MeetingMinutes>>(meetingMinutesJson);
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
    }
}
