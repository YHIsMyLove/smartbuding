using System;
using SmartConstructionServices.Account.Models;
using SmartConstructionServices.ProjectManagement.Models;

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

        public string SessionID { get; set; }

        public bool IsLogin()
        {
            return CurrentUser != null;
        }

        private static ServiceContext instance;
    }
}
