using Newtonsoft.Json;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.ProjectManagement.Models;
using System;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.Common
{
    public class ServiceContext
    {
        public const string AppKey = "443eed7d6dab47739915d6a237dcad34";

        private ServiceContext()
        {
        }

        public static ServiceContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new ServiceContext();
                return instance;
            }
        }

        public User CurrentUser
        {
            get;
            set;
        }

        public string Region { get; set; }

        public Project CurrentProject
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("Proj"))
                {
                    var projJson = Application.Current.Properties["Proj"] as string;
                    return JsonConvert.DeserializeObject<Project>(projJson);
                }
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    var projJson = JsonConvert.SerializeObject(value);
                    Application.Current.Properties["Proj"] = projJson;
                }
            }
        }

        public string SessionID
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("SessionID"))
                    return (string)Application.Current.Properties["SessionID"];
                else
                    return null;
            }
            set { Application.Current.Properties["SessionID"] = value; }
        }
        /// <summary>
        /// 萤石云token
        /// </summary>
        /// <value>The YSA ccess token.</value>
		public string YSAccessToken
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("YSAccessToken"))
                    return (string)Application.Current.Properties["YSAccessToken"];
                else
                    return null;
            }
            set { Application.Current.Properties["YSAccessToken"] = value; }
        }

        /// <summary>
        /// 当前最新会议的时间
        /// </summary>
        /// <value>The latest meeting time.</value>
        public DateTime LatestMeetingTime
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("LatestMeetingTime"))
                    return DateTime.Parse((string)Application.Current.Properties["LatestMeetingTime"]);
                else
                    return DateTime.MinValue;
            }
            set
            {
                Application.Current.Properties["LatestMeetingTime"] = value.ToString();
            }
        }

		public bool IsLogin()
        {
            return CurrentUser != null;
        }

        private static ServiceContext instance;
    }
}
