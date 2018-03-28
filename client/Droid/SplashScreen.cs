
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace SmartConstructionSite.Droid
{
    [Activity(Label = "SplashScreen", MainLauncher = true, Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SplashScreen : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //RequestWindowFeature(WindowFeatures.NoTitle);
            //Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
                //Window.DecorView.SystemUiVisibility = StatusBarVisibility.Hidden;

            //var uiOpts = SystemUiFlags.LayoutStable 
            //                          | SystemUiFlags.LayoutHideNavigation
            //                          | SystemUiFlags.LayoutFullscreen
            //                          | SystemUiFlags.Fullscreen
            //                          | SystemUiFlags.HideNavigation
            //                          | SystemUiFlags.Immersive;
            //Window.DecorView.SystemUiVisibility = uiOpts;

            SetContentView(Resource.Layout.layout_splash_screen);

            new Handler().PostDelayed(() =>
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }, 3000);
        }
    }
}
