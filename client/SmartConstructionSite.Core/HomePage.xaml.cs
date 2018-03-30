using BottomBar.XamarinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace SmartConstructionSite.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : BottomBarPage
    {
		public HomePage ()
		{
			InitializeComponent ();
		}

        async void Button_Clicked(object sender, EventArgs args)
        {
            if (sender == btnScan)
            {
                var scanPage = new ZXingScannerPage() { Title = "扫描" };

                scanPage.OnScanResult += (result) => {
                    // Stop scanning
                    scanPage.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PopAsync();
                        DisplayAlert("扫描结果", result.Text, "确定");
                    });
                };

                // Navigate to our scanner page
                await Navigation.PushAsync(scanPage);
            }
        }
	}
}