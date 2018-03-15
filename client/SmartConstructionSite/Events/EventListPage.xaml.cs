using SmartConstructionServices.Events.Models;
using SmartConstructionServices.Events.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SmartConstructionSite.Events
{
    public partial class EventListPage : ContentPage
    {
        EventListViewModel viewModel;

        public EventListPage()
        {
            viewModel = new EventListViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.FetchLatestEventsCommand.Execute(null);
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
    }
}
