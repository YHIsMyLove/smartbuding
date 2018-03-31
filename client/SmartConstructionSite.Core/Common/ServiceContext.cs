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

        public Project CurrentProject { get; set; }

        public string SessionID
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("SessionID"))
                    return Application.Current.Properties["SessionID"] as string;
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
                    return Application.Current.Properties["YSAccessToken"] as string;
                else
                    return null;
            }
            set { Application.Current.Properties["YSAccessToken"] = value; }
        }

		public bool IsLogin()
        {
            return CurrentUser != null;
        }

        private static ServiceContext instance;
    }
}
