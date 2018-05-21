using Acr.UserDialogs;
using SmartConstructionSite.Core.Account.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.Account.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private LoginViewModel viewModel;

        public LoginPage()
        {
            viewModel = new LoginViewModel();
            BindingContext = viewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.IsLoginSucceed) && viewModel.IsLoginSucceed)
            {
                //Application.Current.Properties["SessionId"] = ServiceContext.Instance.CurrentUser.SessionId;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Task.Delay(200);
                await Navigation.PopAsync(true);
            }
            else if (e.PropertyName == nameof(viewModel.IsBusy))
            {
                if (viewModel.IsBusy)
                    UserDialogs.Instance.ShowLoading("正在登陆。。。", MaskType.Black);
                else
                    UserDialogs.Instance.HideLoading();
            }
            else if (e.PropertyName == nameof(viewModel.Error) && viewModel.Error != null)
            {
                var toastConfig = new ToastConfig(viewModel.Error.Description);
                toastConfig.SetDuration(3000);
                UserDialogs.Instance.Toast(toastConfig);
            }
        }
    }
}