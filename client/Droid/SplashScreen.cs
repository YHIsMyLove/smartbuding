
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace SmartConstructionSite.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SplashScreen : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_splash_screen);

            var uiOpts = SystemUiFlags.LayoutStable 
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Immersive;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOpts;

            new Handler().PostDelayed(() =>
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }, 3000);
        }
    }
}
