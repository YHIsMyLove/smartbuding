using System;
using System.Collections.Generic;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.DoorMonitor.ViewModels;
using Xamarin.Forms;
//using Microcharts;
//using SkiaSharp;

namespace SmartConstructionSite.Core.DoorMonitor.Views
{
    public partial class DoorMonitorRecordPage : ContentPage
    {
		DoorMonitorRecordViewModel viewModel;

        public DoorMonitorRecordPage()
        {
			viewModel = new DoorMonitorRecordViewModel();
            BindingContext = viewModel;

			InitializeComponent();

			//var source = new HtmlWebViewSource();
			//source.BaseUrl = DependencyService.Get<IBaseUrl>()?.Get();
			//source.Html = DependencyService.Get<ISaveAndLoad>()?.LoadAsset("PieChart.html");
			//webView.Source = source;
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			listView.SelectedItem = null;
		}

		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new StatisticsListPage(), true);
		}
    }
}
