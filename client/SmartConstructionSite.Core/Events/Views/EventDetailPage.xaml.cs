using Acr.UserDialogs;
using BottomBar.XamarinForms;
using SmartConstructionSite.Core.Events.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.Events.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : BottomBarPage
    {
        private EventDetailViewModel viewModel;

        public EventDetailPage()
        {
            //viewModel = new EventDetailViewModel();
            //viewModel.PropertyChanged += ViewModel_PropertyChanged;
            //BindingContext = viewModel;
            InitializeComponent();
            BindingContextChanged += EventDetailPage_BindingContextChanged;
        }

        private void EventDetailPage_BindingContextChanged(object sender, EventArgs e)
        {
            viewModel = BindingContext as EventDetailViewModel;
            if (viewModel != null)
                viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel != null)
                viewModel.RefreshCommand.Execute(null);
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.IsBusy))
            {
                if (viewModel.IsBusy)
                    UserDialogs.Instance.ShowLoading("正在查询。。。", MaskType.Black);
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