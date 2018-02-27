using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using SmartConstructionSite.Droid.OnlineMonitoring;

namespace SmartConstructionSite.Droid
{
    [Activity(Label = "SmartConstructionSite.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string AppKey = "443eed7d6dab47739915d6a237dcad34";
        const string AccessTokenForTest = "at.at971uw757e0x6njd2t6tghbcbpw4mve-2vc2k9bc1r-13uq4rh-lua3ahwpa";
        static bool ezopenSDKInitilized;
        App app;

        protected override void OnCreate(Bundle bundle)
        {
            InitSDK();

            //EZOpenSDK.Instance.SetAccessToken(AccessTokenForTest);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarians.MediaPlayer.Droid.VideoPlayerRenderer.Init(this);

            app = new App();
            app.SetFullScreen(false);
            app.FullScreenRequested += App_FullScreenRequested;
            app.LandscapeRequested += App_LandscapeRequested;
            app.CameraHelper.ShowCameraListRequested += CameraHelper_ShowCameraListRequested;

            LoadApplication(app);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            app.FullScreenRequested -= App_FullScreenRequested;
            app.LandscapeRequested -= App_LandscapeRequested;
            app.CameraHelper.ShowCameraListRequested -= CameraHelper_ShowCameraListRequested;
        }

        private void InitSDK()
        {
            //if (ezopenSDKInitilized) return;
            ///**
            // * sdk日志开关，正式发布需要去掉
            // */
            //EZOpenSDK.ShowSDKLog(true);

            ///**
            // * 设置是否支持P2P取流,详见api
            // */
            //EZOpenSDK.EnableP2P(true);

            ///**
            // * APP_KEY请替换成自己申请的
            // */
            //EZOpenSDK.InitLib(Application, AppKey, "");

            //ezopenSDKInitilized = true;
        }

        /// <summary>
        /// Show camera list
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void CameraHelper_ShowCameraListRequested(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(TestActivity)));
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
