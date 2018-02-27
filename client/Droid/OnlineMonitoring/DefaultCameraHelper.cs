using System;
using Com.Videogo.Openapi;
using SmartConstructionSite.OnlineMonitoring;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    public class DefaultCameraHelper : CameraHelper
    {
        const string AppKey = "";

        public DefaultCameraHelper()
        {
            
        }

        public void ShowCameraList()
        {
            throw new NotImplementedException();
        }

        private void initSDK()
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
            EZOpenSDK.InitLib(this, AppKey);
        }
    }
}
