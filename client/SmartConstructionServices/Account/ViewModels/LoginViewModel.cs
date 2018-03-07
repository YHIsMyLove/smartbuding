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
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            userService = new UserService();
            LoginCommand = new Command(execute: () => { Login(); }, canExecute: () => { return IsLoginCommandCanExecute(); });
        }

        private async Task Login()
        {
            if (loginPending) return;
            loginPending = true;
            var result = await userService.Login(username, password);
            loginPending = false;
            User user = result.Model;
        }

        #region Properties
        public string Username
        {
            get { return username; }
            set
            {
                if (username == value) return;
                username = value;
                NotifyPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password == value) return;
                password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public bool LoginPending
        {
            get { return loginPending; }
            set
            {
                if (loginPending == value) return;
                loginPending = value;
                NotifyPropertyChanged("LoginPending");
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
                          && !loginPending;
        }

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private UserService userService;
        private string username;
        private string password;
        private bool loginPending;
    }
}
