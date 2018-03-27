using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartConstructionServices.Account.Services;
using SmartConstructionServices.Common;
using Xamarin.Forms;

namespace SmartConstructionServices.Account.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            userService = new UserService();
            LoginCommand = new Command(execute: async () => { await Login(); }, canExecute: () => { return IsLoginCommandCanExecute(); });
        }

        private async Task Login()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            var result = await userService.Login(username, password);
            IsBusy = false;
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
            }
            else
            {
                //ServiceContext.Instance.CurrentUser = result.Model;
                ServiceContext.Instance.Region = "湖北省黄石市";
                ServiceContext.Instance.CurrentProject = SimpleData.Instance.GetProjects(ServiceContext.Instance.Region)[0];
                IsLoginSucceed = true;
            }
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
        private string username = "admin";
        private string password = "admin";
        private bool isLoginSucceed;
    }
}
