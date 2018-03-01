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
using Com.Videogo.Openapi.Bean;
using System.Threading.Tasks;
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
    }
}