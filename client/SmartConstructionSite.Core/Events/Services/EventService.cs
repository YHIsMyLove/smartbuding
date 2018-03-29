using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Events.Services
{
    public class EventService
    {
        public Task<Result<IList<PageTypeGroup>>> FetchLatestEvent()
        {
            return Task.Run(() =>
            {
                var result = new Result<IList<PageTypeGroup>>();
                var meetings = SimpleData.Instance.GetMeetings();
                result.Model = meetings
                .GroupBy(i => i.Time.ToString("yyyy年MM月"))
                .Select(i =>
                {
                    var group = new PageTypeGroup(i.Key, "");
                    group.AddRange(i);
                    return group;
                }).ToList();
                return result;
            });
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
