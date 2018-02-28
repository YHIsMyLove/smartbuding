
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
using Com.Videogo.Openapi;
using Com.Videogo.Openapi.Bean;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    [Activity(Label = "CameraListActivity")]
    public class CameraListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_camera_list);
            InitViews();
            InitData();
        }

        private void InitData()
        {
            
        }

        //private async List<EZDeviceInfo> FetchCameraList()
        //{
        //    await EZOpenSDK.Instance.GetDeviceList(0, 10);
        //}

        private void InitViews()
        {
            listViewCamera = (ListView)FindViewById(Resource.Id.listViewCamera);
        }

        private ListView listViewCamera;
    }
}
