using SmartConstructionServices.Account.ViewModels;
using SmartConstructionServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Account
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private LoginViewModel viewModel;

        public LoginPage ()
		{
            viewModel = new LoginViewModel();
            BindingContext = viewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
			InitializeComponent ();
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
        }
    }
}