using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using SmartConstructionSite.Droid.OnlineMonitoring;
using Xamarin.Forms;
using Android.Runtime;
using Plugin.Permissions;
using SmartConstructionSite.Core;

namespace SmartConstructionSite.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        App app;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            
            //Window.AddFlags(WindowManagerFlags.Fullscreen);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            app = new App();
            app.SetFullScreen(false);
            app.FullScreenRequested += App_FullScreenRequested;
            app.LandscapeRequested += App_LandscapeRequested;
            app.ShowCameraListRequested += CameraHelper_ShowCameraListRequested;

            LoadApplication(app);
        }

        protected void HideBottomUIMenu()
        {
            //隐藏虚拟按键，并且全屏
            if ((int)Build.VERSION.SdkInt > 11 && (int)Build.VERSION.SdkInt < 19)
            { // lower api
                Android.Views.View v = Window.DecorView;
                v.SystemUiVisibility = StatusBarVisibility.Hidden;
            }        
            else if ((int)Build.VERSION.SdkInt >= 19)
            {
                //for new api versions.
                Android.Views.View decorView = Window.DecorView;
                decorView.SystemUiVisibility = StatusBarVisibility.Hidden;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            app.FullScreenRequested -= App_FullScreenRequested;
            app.LandscapeRequested -= App_LandscapeRequested;
            //app.CameraHelper.ShowCameraListRequested -= CameraHelper_ShowCameraListRequested;
        }

        /// <summary>
        /// Show camera list
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void CameraHelper_ShowCameraListRequested(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(CameraListActivity)));
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
