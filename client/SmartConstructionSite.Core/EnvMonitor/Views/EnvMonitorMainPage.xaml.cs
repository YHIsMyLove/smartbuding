using System;
using System.Collections.Generic;
using SmartConstructionSite.Core.Common;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.EnvMonitor.Views
{
    public partial class EnvMonitorMainPage : ContentPage
    {
        public EnvMonitorMainPage()
        {
            InitializeComponent();

            var source = new HtmlWebViewSource();
            source.Html = LoadHtml();
            source.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            chartContainer.Source = source;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        private string LoadHtml()
        {
            return DependencyService.Get<ISaveAndLoad>().LoadAsset("BarChart.html");
        }
    }
}
