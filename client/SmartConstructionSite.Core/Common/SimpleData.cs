using SmartConstructionSite.Core.Events.Models;
using SmartConstructionSite.Core.PeopleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Common
{
    internal class SimpleData
    {

        private readonly Dictionary<string, List<string>> projectDic = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> departmentDic = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<Contacts>> projectContactsDic = new Dictionary<string, List<Contacts>>();
        private readonly Dictionary<string, List<Contacts>> departmentContactsDic = new Dictionary<string, List<Contacts>>();
        private readonly Dictionary<Meeting, List<MeetingMinutes>> meetingMinutesDic = new Dictionary<Meeting, List<MeetingMinutes>>();
        private readonly Dictionary<string, List<string>> cityDic = new Dictionary<string, List<string>>();
        private readonly List<string> years = new List<string>();
        private readonly List<string> months = new List<string>();

        public static SimpleData Instance
        {
            get {
                if (instance == null)
                    instance = new SimpleData();
                return instance;
            }
        }

        public List<string> GetProvinces()
        {
            return cityDic.Keys.ToList();
        }

        public List<string> GetCities(string province)
        {
            if (cityDic.ContainsKey(province))
                return cityDic[province];
            return new List<string>();
        }

        public List<string> GetProjects(string province)
        {
            if (projectDic.ContainsKey(province))
            {
                List<string> list = projectDic[province];
                list.Remove("无");
                return list;
            }
            else
                return new List<string>();
        }

        public List<string> GetProjects(string province, string city)
        {
            string region = province + city;
            if (projectDic.ContainsKey(region))
            {
                List<string> list = projectDic[region];
                list.Remove("无");
                return list;
            }
            else
                return new List<string>();
        }

        public List<string> GetDepartments(string project)
        {
            if (departmentDic.ContainsKey(project))
                return departmentDic[project];
            else
                return new List<string>();
        }

        public List<Contacts> GetContacts(string project)
        {
            if (projectContactsDic.ContainsKey(project))
                return projectContactsDic[project];
            return new List<Contacts>();
        }

        public List<Contacts> GetContacts(string project, string department)
        {
            if (departmentContactsDic.ContainsKey(project + "_" + department))
                return departmentContactsDic[project + "_" + department];
            return new List<Contacts>();
        }

        public List<Meeting> GetMeetings()
        {
            return meetingMinutesDic.Keys.ToList();
        }

        public List<MeetingMinutes> GetMeetingMinutes()
        {
            return meetingMinutesDic.Values.FirstOrDefault(); ;
        }

        public List<string> GetYears()
        {
            return years;
        }

        public List<string> GetMonths()
        {
            return months;
        }

        public List<string> GetDays(int year, int month)
        {
            int count = DateTime.DaysInMonth(year, month);
            var days = new List<string>();
            days.Add("无");
            for (int i = 1; i <= count; i++)
            {
                days.Add(i.ToString());
            }
            return days;
        }

        private SimpleData()
        {
            List<string> cities1 = new List<string>() { "黄石市", "武汉市" };
            cityDic.Add("湖北省", cities1);
            List<string> cities2 = new List<string>() { "长沙市", "郴州市" };
            cityDic.Add("湖南省", cities2);

            List<string> projects = new List<string>()
            {
                "无", "黄石奥体中心建设项目", "黄石慈湖剧院飞天白鹭建设项目",
            };
            projectDic.Add("湖北省黄石市", projects);
            projectDic.Add("湖北省", projects);

            List<string> departments1 = new List<string>()
            {
                "无", "经营部", "设计部", "工程部"
            };
            departmentDic.Add(projects[1], departments1);
            List<string> departments2 = new List<string>()
            {
                "无", "经营部", "设计部"
            };
            departmentDic.Add(projects[2], departments2);

            List<Contacts> contacts1 = new List<Contacts>
            {
                new Contacts(){ Name = "张三", Position = "设计师", Department = departments1[1], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三2", Position = "设计师", Department = departments1[1], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三3", Position = "设计师", Department = departments1[1], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三4", Position = "设计师", Department = departments1[1], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三5", Position = "设计师", Department = departments1[1], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三6", Position = "设计师", Department = departments1[1], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三7", Position = "设计师", Department = departments1[2], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三8", Position = "设计师", Department = departments1[2], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三9", Position = "设计师", Department = departments1[2], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三10", Position = "设计师", Department = departments1[2], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "张三11", Position = "设计师", Department = departments1[3], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "李四", Position = "设计师", Department = departments1[3], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "李四2", Position = "设计师", Department = departments1[3], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "李四3", Position = "设计师", Department = departments1[3], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
                new Contacts(){ Name = "李四4", Position = "设计师", Department = departments1[3], PhotoUrl = "", PhoneNumber = "13339809878", IdCard = "429004198609194675", Email = "13345649987@163.com" },
            };
            projectContactsDic.Add(projects[1], contacts1);

            List<Contacts> contacts2 = new List<Contacts>
            {
                contacts1[0], contacts1[1], contacts1[2], contacts1[3], contacts1[4], contacts1[5]
            };
            departmentContactsDic.Add(projects[1] + "_" + departments1[1], contacts2);

            List<Contacts> contacts3 = new List<Contacts>
            {
                contacts1[6], contacts1[7], contacts1[8], contacts1[9]
            };
            departmentContactsDic.Add(projects[1] + "_" + departments1[2], contacts3);

            List<Contacts> contacts4 = new List<Contacts>
            {
                contacts1[10], contacts1[11], contacts1[12], contacts1[13], contacts1[14]
            };
            departmentContactsDic.Add(projects[1] + "_" + departments1[3], contacts4);

            List<Meeting> meetings = new List<Meeting>()
            {
                new Meeting(){ MeetingName = "三月项目成本核算会议", Compere = "张三", MeetingCreatedAt = new DateTime(2017, 3, 6), RelationalCount = 2 },
                new Meeting(){ MeetingName = "安全生产培训会议", Compere = "张三", MeetingCreatedAt = new DateTime(2017, 3, 16), RelationalCount = 1 },
                new Meeting(){ MeetingName = "三月施工现场安全检查会议", Compere = "张三", MeetingCreatedAt = new DateTime(2017, 3, 6), RelationalCount = 0 },
                new Meeting(){ MeetingName = "三月施工现场安全检查会议", Compere = "张三", MeetingCreatedAt = new DateTime(2017, 2, 6), RelationalCount = 0 },
            };
            List<MeetingMinutes> minutes1 = new List<MeetingMinutes>()
            {
                new MeetingMinutes(){ Content = "作为一个跨平台开发框架，Xamarin.Mobile有很多优点。在这一框架内，开发iOS、Android、Windows Phone和Mac App应用可以不用转到Eclipse 或者额外购买Mac并使用Xcode，而继续在Visual Studio之中使用C#与.NET Framework进行。", IsRelational = true },
                new MeetingMinutes(){ Content = "作为一个跨平台开发框架，Xamarin.Mobile有很多优点。在这一框架内，开发iOS、Android、Windows Phone和Mac App应用可以不用转到Eclipse 或者额外购买Mac并使用Xcode，而继续在Visual Studio之中使用C#与.NET Framework进行。", IsRelational = true },
                new MeetingMinutes(){ Content = "作为一个跨平台开发框架，Xamarin.Mobile有很多优点。在这一框架内，开发iOS、Android、Windows Phone和Mac App应用可以不用转到Eclipse 或者额外购买Mac并使用Xcode，而继续在Visual Studio之中使用C#与.NET Framework进行。", IsRelational = false },
                new MeetingMinutes(){ Content = "作为一个跨平台开发框架，Xamarin.Mobile有很多优点。在这一框架内，开发iOS、Android、Windows Phone和Mac App应用可以不用转到Eclipse 或者额外购买Mac并使用Xcode，而继续在Visual Studio之中使用C#与.NET Framework进行。" }
            };
            meetingMinutesDic.Add(meetings[0], minutes1);
            meetingMinutesDic.Add(meetings[1], minutes1);
            meetingMinutesDic.Add(meetings[2], minutes1);
            meetingMinutesDic.Add(meetings[3], minutes1);

            years.Add("无");
            for (int i = 1902; i <= 2037; i++)
            {
                years.Add(i.ToString());
            }
            months.Add("无");
            for (int i = 1; i < 13; i++)
            {
                months.Add(i.ToString());
            }
        }

        private static SimpleData instance;
    }
}
