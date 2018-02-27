using System;
using Android.App;
using Com.Videogo.Openapi;

[assembly: Application(
    Debuggable = true,
    Label = "SmartConstructionSite",
    ManageSpaceActivity = typeof(SmartConstructionSite.Droid.MyApplication))]
namespace SmartConstructionSite.Droid
{
    public class MyApplication : Application
    {
        const string AppKey = "443eed7d6dab47739915d6a237dcad34";

        public MyApplication()
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            InitSDK();
        }

        private void InitSDK()
        {
            /**
             * sdk日志开关，正式发布需要去掉
             */
            EZOpenSDK.ShowSDKLog(true);

            /**
             * 设置是否支持P2P取流,详见api
             */
            EZOpenSDK.EnableP2P(true);

            /**
             * APP_KEY请替换成自己申请的
             */
            EZOpenSDK.InitLib(this, AppKey, "");
        }
    }
}
