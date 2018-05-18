using System;
using SmartConstructionServices.Account.Models;
using SmartConstructionServices.ProjectManagement.Models;
using Xamarin.Forms;

namespace SmartConstructionServices.Common
{
    public class ServiceContext
    {
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

        public string CurrentProject { get; set; }

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
