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
        public static Task<Core.Common.Result<IList<EZDeviceInfo>>> FetchCameraList(int page, int pageSize)
        {
            return Task.Run(() =>
            {
                var result = new Core.Common.Result<IList<EZDeviceInfo>>();
                try
                {
                    result.Model = EZOpenSDK.Instance.GetDeviceList(page, pageSize);
                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Error = new Core.Common.Error() { Description = e.Message, Exception = e };
                }
                return result;
            });
        }

        internal static Task<Core.Common.Result<bool>> SetVideoLevel(string deviceSerial, int cameraNo, EZConstants.EZVideoLevel videoLevel)
        {
            return Task.Run(()=>
            {
                var result = new Core.Common.Result<bool>();
                try
                {
                    var state = EZOpenSDK.Instance.SetVideoLevel(deviceSerial, cameraNo, videoLevel.Ordinal());
                    result.Model = state;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Fail(e.Message);
                    result.HasError = true;
                    result.Error = new Core.Common.Error() { Description = e.Message, Exception = e };
                }
                return result;
            });
        }

        internal static Task<Core.Common.Result<bool>> ControlPTZ(string deviceSerial, int cameraNo, EZConstants.EZPTZCommand cmd, EZConstants.EZPTZAction action, int ptzSpeedDefault)
        {
            return Task.Run(() =>
            {
                var result = new Core.Common.Result<bool>();
                try
                {
                    var state = EZOpenSDK.Instance.ControlPTZ(deviceSerial, cameraNo, cmd, action, EZConstants.PtzSpeedDefault);
                    result.Model = state;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    result.HasError = true;
                    result.Error = new Core.Common.Error() { Description = e.Message, Exception = e };
                }
                return result;
            });
        }
    }
}