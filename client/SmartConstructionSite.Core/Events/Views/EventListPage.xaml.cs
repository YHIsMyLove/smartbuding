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
            BindingContext = viewModel;
            InitializeComponent();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Meeting meeting = e.Item as Meeting;
            if (meeting == null) return;

            EventDetailViewModel detailViewModel = new EventDetailViewModel();
            detailViewModel.Meeting = meeting;
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