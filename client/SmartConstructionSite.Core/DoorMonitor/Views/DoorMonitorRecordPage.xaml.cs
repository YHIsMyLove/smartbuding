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

			var source = new HtmlWebViewSource();
			source.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
			source.Html = DependencyService.Get<ISaveAndLoad>().LoadAsset("PieChart.html");
			webView.Source = source;
        }

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			listView.SelectedItem = null;
		}
    }
}
