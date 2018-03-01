
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
using Com.Videogo.Constant;

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

        private async void InitData()
        {
            IList<EZDeviceInfo> list = await CameraHelpers.FetchCameraList(0, 10);
            cameraListAdapter.SetCameras(list);
        }

        private void InitViews()
        {
            listViewCamera = FindViewById<ListView>(Resource.Id.listViewCamera);
            listViewCamera.ItemClick += ListViewCamera_ItemClick;
            cameraListAdapter = new CameraListAdapter(this);
            listViewCamera.Adapter = cameraListAdapter;
        }

        private void ListViewCamera_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            EZDeviceInfo device = (EZDeviceInfo)cameraListAdapter.GetItem(e.Position);
            if (device.Status == 2)
            {
                Toast.MakeText(this, "设备不在线", ToastLength.Long).Show();
            }
            else
            {
                if (device.CameraNum <= 0) return;
                EZCameraInfo camera = device.CameraInfoList[0];
                Intent intent = new Intent(this, typeof(CameraLiveStreamingActivity));
                intent.PutExtra(IntentConsts.ExtraDeviceInfo, device);
                intent.PutExtra(IntentConsts.ExtraCameraInfo, camera);
                StartActivity(intent);
            }
        }

        private ListView listViewCamera;
        private CameraListAdapter cameraListAdapter;
    }
}
