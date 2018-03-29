using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Account.Services;
using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.Account.ViewModels
{
    public class UserMainViewModel : ViewModelBase
    {
        public UserMainViewModel()
        {
            userService = new UserService();
            LogoutCommand = new Command(
                execute: async () =>
                {
                    await Logout();
                },
                canExecute: () => { return IsLogoutCommandCanExecute(); });
            User = ServiceContext.Instance.CurrentUser;
        }

        #region Properties

        public bool IsLogoutSucceed {
            get { return isLogoutSucceed; }
            set {
                if (isLogoutSucceed == value) return;
                isLogoutSucceed = value;
                NotifyPropertyChanged(nameof(IsLogoutSucceed));
            }
        }

        public User User {
            get { return user; }
            set {
                if (user == value) return;
                user = value;
                NotifyPropertyChanged(nameof(User));
            }
        }

        #endregion

        #region Commands

        public ICommand LogoutCommand { get; private set; }

        #endregion

        #region Previte methods

        private void RefreshCommandCanExecute()
        {
            ((Command)LogoutCommand).ChangeCanExecute();
        }

        private bool IsLogoutCommandCanExecute()
        {
            return !IsBusy;
        }

        private async Task Logout()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            RefreshCommandCanExecute();
            var result = await userService.Logout();
            IsBusy = false;
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
            }
            else
            {
                ServiceContext.Instance.CurrentUser = null;
                IsLogoutSucceed = true;
            }
        }

        #endregion

        #region Fields

        private UserService userService;
        private bool isLogoutSucceed;
        private User user;

        #endregion
    }
}
