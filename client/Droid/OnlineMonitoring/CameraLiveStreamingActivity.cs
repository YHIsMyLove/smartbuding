
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Ezvizuikit.Open;
using Java.Util;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    [Activity(Label = "CameraLiveStreamingActivity")]
    public class CameraLiveStreamingActivity : Activity, EZUIPlayer.IEZUIPlayerCallBack
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_camera_live_streaming);
            InitView();
        }

        private void InitView()
        {
            player = (EZUIPlayer)FindViewById(Resource.Id.player);
            EZUIKit.InitWithAppKey(Application, MainActivity.AppKey);
            EZUIKit.SetAccessToken(MainActivity.AccessTokenForTest);
            player.SetCallBack(this);
            player.SetUrl("ezopen://open.ys7.com/135665567/1.live");
            SetSurfaceSize();
        }

        public void OnPlayFail(EZUIError p0)
        {
            Toast.MakeText(this, "播放错误。错误描述：" + p0.ErrorString, ToastLength.Long).Show();
        }

        public void OnPlayFinish()
        {
            Log.Debug(tag, "OnPlaySuccess");
        }

        public void OnPlaySuccess()
        {
            Log.Debug(tag, "OnPlaySuccess");
        }

        public void OnPlayTime(Calendar p0)
        {
            Log.Debug(tag, "OnPlayTime.time: {0}", p0.Time.ToString());
        }

        public void OnPrepared()
        {
            Log.Debug(tag, "OnPrepared");
            player.StartPlay();
        }

        public void OnVideoSizeChange(int p0, int p1)
        {
            Log.Debug(tag, "OnVideoSizeChange.new size x: {0}, y: {1}", p0, p1);
            SetSurfaceSize();
        }

        void SetSurfaceSize()
        {
            DisplayMetrics dm = new DisplayMetrics();
            IWindowManager winMgr = Window.WindowManager;
            winMgr.DefaultDisplay.GetMetrics(dm);
            player.SetSurfaceSize(dm.WidthPixels, dm.WidthPixels > dm.HeightPixels ? dm.HeightPixels : 0);
        }

        private static readonly string tag = typeof(CameraLiveStreamingActivity).Name;
        private EZUIPlayer player;
    }
}
