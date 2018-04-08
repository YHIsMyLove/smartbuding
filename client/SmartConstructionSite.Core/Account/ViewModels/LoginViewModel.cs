using SmartConstructionSite.Core.Account.Services;
using SmartConstructionSite.Core.Common;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.Account.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            userService = new UserService();
            LoginCommand = new Command(execute: async () => { await Login(); }, canExecute: () => { return IsLoginCommandCanExecute(); });

            if (Application.Current.Properties.ContainsKey("Username"))
                username = (string)Application.Current.Properties["Username"];
            if (Application.Current.Properties.ContainsKey("Password"))
                password = (string)Application.Current.Properties["Password"];
        }

        private async Task Login()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            var result = await userService.Login(username, password);
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
            }
            else
            {
                Application.Current.Properties["Username"] = username;
                Application.Current.Properties["Password"] = password;
                await Application.Current.SavePropertiesAsync();
                var result1 = await userService.GetUserInfo(ServiceContext.Instance.SessionID);
                
                if (result1.HasError)
                {
                    HasError = true;
                    Error = result.Error;
                }
                else
                {
                    ServiceContext.Instance.CurrentUser = result1.Model;
                    //ServiceContext.Instance.Region = SimpleData.Instance.GetProvinces()[0];
                    //ServiceContext.Instance.CurrentProject = SimpleData.Instance.GetProjects(ServiceContext.Instance.Region)[0];
                    //ServiceContext.Instance.CurrentProject = new ProjectManagement.Models.Project(){Name="黄石奥体中心项目"};
                    IsLoginSucceed = true;
                }
            }
            IsBusy = false;
        }

        #region Properties
        public string Username {
            get { return username; }
            set {
                if (username == value) return;
                username = value;
                NotifyPropertyChanged(nameof(Username));
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public string Password {
            get { return password; }
            set {
                if (password == value) return;
                password = value;
                NotifyPropertyChanged(nameof(Password));
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public bool IsLoginSucceed {
            get { return isLoginSucceed; }
            set {
                if (isLoginSucceed == value) return;
                isLoginSucceed = value;
                NotifyPropertyChanged(nameof(IsLoginSucceed));
            }
        }
        #endregion

        #region Commands

        public ICommand LoginCommand { get; private set; }

        #endregion

        private bool IsLoginCommandCanExecute()
        {
            return !string.IsNullOrEmpty(username)
                          && !string.IsNullOrEmpty(password)
                          && !IsBusy;
        }

        private UserService userService;
        private string username = "lbgongfu";
        private string password = "123456";
        private bool isLoginSucceed;
    }
}
