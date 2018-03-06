using System;
using SmartConstructionServices.Account.Models;

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
            private set;
        }

        public bool IsLogin()
        {
            return CurrentUser != null;
        }

        private static ServiceContext instance;
    }
}
