using System;
using System.Collections.Generic;
using SmartConstructionSite.Core.DoorMonitor.ViewModels;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.DoorMonitor.Views
{
    public partial class StatisticsListPage : ContentPage
    {
		StatisticsListViewModel viewModel;

        public StatisticsListPage()
        {
			viewModel = new StatisticsListViewModel();
			BindingContext = viewModel;
            InitializeComponent();
        }

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			listView.SelectedItem = null;
		}
    }
}
