using System;
using System.Collections.Generic;
using SmartConstructionSite.Core.Rankings.ViewModels;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.Rankings.Views
{
    public partial class RedRankingListPage : ContentPage
    {
        RedRankingListViewModel viewModel;

        public RedRankingListPage()
        {
            viewModel = new RedRankingListViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }
    }
}
