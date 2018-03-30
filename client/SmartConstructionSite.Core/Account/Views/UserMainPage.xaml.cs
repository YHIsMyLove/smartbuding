using Acr.UserDialogs;
using SmartConstructionSite.Core.Account.ViewModels;
using SmartConstructionSite.Core.Common;
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
	public partial class UserMainPage : ContentPage
	{
        private UserMainViewModel viewModel;

        public UserMainPage()
        {
            viewModel = new UserMainViewModel();
            BindingContext = viewModel;
            InitializeComponent();
            if (ServiceContext.Instance.CurrentUser != null)
            {
                //imageUserPhoto.Source = new UriImageSource
                //{
                //    Uri = new Uri(ServiceContext.Instance.CurrentUser.UserHeadImg),
                //    CachingEnabled = true,
                //    CacheValidity = new TimeSpan(5, 0, 0, 0)
                //};
            }
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.IsBusy))
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

        public ListView ListView {
            get { return listView; }
        }
    }
}