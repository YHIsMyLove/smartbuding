using System;
using System.Collections.Generic;
using SmartConstructionSite.Core.Common;
using Xamarin.Forms;
//using Microcharts;
//using SkiaSharp;

namespace SmartConstructionSite.Core.DoorMonitor.Views
{
    public partial class DoorMonitorRecordPage : ContentPage
    {
        public DoorMonitorRecordPage()
        {
            InitializeComponent();

            var source = new HtmlWebViewSource();
            source.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            source.Html = DependencyService.Get<ISaveAndLoad>().LoadAsset("PieChart.html");
            webView.Source = source;
        }
    }
}
