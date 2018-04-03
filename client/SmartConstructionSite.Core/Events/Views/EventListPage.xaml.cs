using Acr.UserDialogs;
using SmartConstructionSite.Core.Events.Models;
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
	public partial class EventListPage : ContentPage
	{
        EventListViewModel viewModel;

        public EventListPage()
        {
            viewModel = new EventListViewModel();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            BindingContext = viewModel;
            InitializeComponent();
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

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Meeting meeting = e.Item as Meeting;
            if (meeting == null) return;

            EventDetailViewModel detailViewModel = new EventDetailViewModel(meeting);
            EventDetailPage detailPage = new EventDetailPage();
            detailPage.BindingContext = detailViewModel;
            await Navigation.PushAsync(detailPage);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }
    }
}