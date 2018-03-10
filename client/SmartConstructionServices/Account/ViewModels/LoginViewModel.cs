using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartConstructionServices.Account.Models;
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
            if (isBusy) return;
            SetProperty(ref isBusy, true, nameof(IsBusy));
            var result = await userService.Login(username, password);
            SetProperty(ref isBusy, false, nameof(IsBusy));
            if (result.HasError)
            {
                SetProperty(ref hasError, true, nameof(HasError));
                SetProperty(ref error, result.Error, nameof(Error));
            }
            else
            {
                ServiceContext.Instance.CurrentUser = result.Model;
                SetProperty(ref isLoginSucceed, true, nameof(IsLoginSucceed));
            }
        }

        #region Properties
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public bool IsLoginSucceed
        {
            get { return isLoginSucceed; }
        }
        #endregion

        #region Commands

        public ICommand LoginCommand { get; private set; }

        #endregion

        private bool IsLoginCommandCanExecute()
        {
            return !string.IsNullOrEmpty(username)
                          && !string.IsNullOrEmpty(password)
                          && !isBusy;
        }

        private UserService userService;
        private string username;
        private string password;
        private bool isLoginSucceed;
    }
}
