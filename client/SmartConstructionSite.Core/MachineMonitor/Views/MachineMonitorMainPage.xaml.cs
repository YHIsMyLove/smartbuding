using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.MachineMonitor.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MachineMonitorMainPage : ContentPage
	{
		public MachineMonitorMainPage ()
		{
			InitializeComponent ();
            var source = new HtmlWebViewSource();
            source.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            source.Html = DependencyService.Get<ISaveAndLoad>().LoadAsset("MachineMonitorPage.html");
            webView.Source = source;
        }
	}
}