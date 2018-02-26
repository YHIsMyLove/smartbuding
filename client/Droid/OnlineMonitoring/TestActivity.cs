using System;
using Android.App;
using Android.Content.PM;
using Android.Widget;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    [Activity(Label = "SmartConstructionSite.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class TestActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public TestActivity()
        {
        }

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TextView textView = new TextView(this);
            textView.Text = "Hello Xamarin";
            SetContentView(textView);
        }
    }
}
