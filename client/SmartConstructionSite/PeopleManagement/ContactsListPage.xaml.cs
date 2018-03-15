﻿using SmartConstructionServices.PeopleManagement.Models;
using SmartConstructionServices.PeopleManagement.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.PeopleManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsListPage : ContentPage
    {
        ContactsListViewModel viewModel;
        public ContactsListPage()
        {
            viewModel = new ContactsListViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.FetchContactsCommand.Execute(null);
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ContactsDetailViewModel detailViewModel = new ContactsDetailViewModel();
            detailViewModel.Contacts = (Contacts)e.Item;
            ContactsDetailPage detailPage = new ContactsDetailPage();
            detailPage.BindingContext = detailViewModel;
            await Navigation.PushAsync(detailPage);
        }
    }
}