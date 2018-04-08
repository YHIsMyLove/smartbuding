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
                    UserDialogs.Instance.ShowLoading("正在注销。。。", MaskType.Black);
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

        private async void Hand_Tapped(object sender, EventArgs args)
        {
            string[] buttons = new string[2];
            buttons[0] = "拍照";
            buttons[1] = "从相册选取";
            var button = await DisplayActionSheet("更换头像", "取消", null, buttons);
            if (button == buttons[0])
            {
                //todo
            }
            else if (button == buttons[1])
            {
                //todo
            }
        }

        public ListView ListView {
            get { return listView; }
        }
    }
}