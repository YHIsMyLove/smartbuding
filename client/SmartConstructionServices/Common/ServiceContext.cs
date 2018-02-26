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

        public void Login(string username, string password)
        {
            CurrentUser = new User();
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        private static ServiceContext instance;
    }
}
