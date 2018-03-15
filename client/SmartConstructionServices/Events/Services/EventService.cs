using SmartConstructionServices.Common;
using SmartConstructionServices.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Events.Services
{
    public class EventService
    {
        public Task<Result<IList<Meeting>>> FetchLatestEvent()
        {
            return Task.Run(() =>
            {
                var result = new Result<IList<Meeting>>();
                result.Model = new List<Meeting>()
                {
                    new Meeting(){ Name = "三月项目成本核算会议", Compere = new PeopleManagement.Models.Contacts(){ Name = "张三" }, Time = new DateTime(2017, 3, 6) },
                    new Meeting(){ Name = "安全生产培训会议", Compere = new PeopleManagement.Models.Contacts(){ Name = "张三" }, Time = new DateTime(2017, 3, 16) },
                    new Meeting(){ Name = "三月施工现场安全检查会议", Compere = new PeopleManagement.Models.Contacts(){ Name = "张三" }, Time = new DateTime(2017, 3, 6) },
                };
                return result;
            });
        }

        public Task<Result<IList<MeetingMinutes>>> FetchMeetingMinutes(Meeting meeting, int page = 1, int pageSize = 10)
        {
            return Task.Run(() =>
            {
                var result = new Result<IList<MeetingMinutes>>();
                result.Model = new List<MeetingMinutes>()
                {
                    new MeetingMinutes(){ Content = "作为一个跨平台开发框架，Xamarin.Mobile有很多优点。在这一框架内，开发iOS、Android、Windows Phone和Mac App应用可以不用转到Eclipse 或者额外购买Mac并使用Xcode，而继续在Visual Studio之中使用C#与.NET Framework进行。", ExecuteDepartments = new List<string>(){ "经营部" } },
                    new MeetingMinutes(){ Content = "作为一个跨平台开发框架，Xamarin.Mobile有很多优点。在这一框架内，开发iOS、Android、Windows Phone和Mac App应用可以不用转到Eclipse 或者额外购买Mac并使用Xcode，而继续在Visual Studio之中使用C#与.NET Framework进行。", ExecuteDepartments = new List<string>(){ "经营部", "工程部" } }
                };
                return result;
            });
        }
    }
}
