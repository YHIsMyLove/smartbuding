using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Com.Videogo.Openapi.Bean;
using Com.Videogo.Openapi;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    public class CameraHelpers
    {
        public static async Task<IList<EZDeviceInfo>> FetchCameraList(int page, int pageSize)
        {
            IList<EZDeviceInfo> list = new List<EZDeviceInfo>();
            await Task.Run(() => { list = EZOpenSDK.Instance.GetDeviceList(page, pageSize); });
            return list;
        }

        internal static async Task<bool> SetVideoLevel(string deviceSerial, int cameraNo, EZConstants.EZVideoLevel videoLevel)
        {
            bool result = true;
            await Task.Run(()=>
            {
                try
                {
                    result = EZOpenSDK.Instance.SetVideoLevel(deviceSerial, cameraNo, videoLevel.Ordinal());
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Fail(e.Message);
                }
            });
            return result;
        }

        internal static async Task<bool> ControlPTZ(string deviceSerial, int cameraNo, EZConstants.EZPTZCommand cmd, EZConstants.EZPTZAction action, int ptzSpeedDefault)
        {
            bool result = true;
            await Task.Run(() =>
            {
                result = EZOpenSDK.Instance.ControlPTZ(deviceSerial, cameraNo, cmd, action, EZConstants.PtzSpeedDefault);
            });
            return result;
        }
    }
}