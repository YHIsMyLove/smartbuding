using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace SmartConstructionSite.Droid
{
    [Activity(Label = "SmartConstructionSite.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        App app;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarians.MediaPlayer.Droid.VideoPlayerRenderer.Init(this);

            app = new App();
            app.SetFullScreen(false);
            app.FullScreenRequested += App_FullScreenRequested;;
            app.LandscapeRequested += App_LandscapeRequested;
            LoadApplication(app);
        }

        void App_FullScreenRequested(object sender, bool fullScreen)
        {
            SetFullScreen(fullScreen);
        }

        void App_LandscapeRequested(object sender, bool landscape)
        {
            RequestedOrientation = landscape ? ScreenOrientation.Landscape : ScreenOrientation.Portrait;
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            //MessagingCenter.Send<MainActivity, Android.Content.Res.Configuration>(this, "ConfigurationChanged", newConfig);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        private void SetFullScreen(bool fullScreen)
        {
            if (fullScreen)
            {
                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            }
            else
            {
                Window.ClearFlags(WindowManagerFlags.Fullscreen);
            }
        }
    }
}
