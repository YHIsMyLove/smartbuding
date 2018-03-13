using SmartConstructionServices.Account.Models;
using SmartConstructionServices.Account.Services;
using SmartConstructionServices.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionServices.Account.ViewModels
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
            SetProperty(ref user, ServiceContext.Instance.CurrentUser, nameof(User));
        }

        #region Properties

        public bool IsLogoutSucceed
        {
            get { return isLogoutSucceed; }
        }

        public User User { get { return user; } set { user = value; } }

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
            return !isBusy;
        }

        private async Task Logout()
        {
            if (isBusy) return;
            SetProperty(ref isBusy, true, "IsBusy");
            RefreshCommandCanExecute();
            var result = await userService.Logout();
            SetProperty(ref isBusy, false, "IsBusy");
            if (result.HasError)
            {
                SetProperty(ref hasError, true, "HasError");
                SetProperty(ref error, result.Error, "Error");
            }
            else
            {
                ServiceContext.Instance.CurrentUser = null;
                SetProperty(ref isLogoutSucceed, true, "IsLogoutSucceed");
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
