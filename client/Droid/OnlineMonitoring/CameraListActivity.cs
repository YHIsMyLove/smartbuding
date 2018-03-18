
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
using Com.Videogo.Openapi;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Android.Content.PM;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    [Activity(Label = "@string/title_dev_list")]
    public class CameraListActivity : Activity
    {
        public const string AppKey = "443eed7d6dab47739915d6a237dcad34";
        public const string AccessTokenForTest = "at.5by3sdd31r32tq7jdoij227j8fmp4qmi-3glqvzc95u-02q3u1s-uayxejlrv";
        static bool ezopenSDKInitilized;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_camera_list);
            InitViews();
            CheckPermissions();
        }

        private void CheckPermissions()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadPhoneState) != Permission.Granted)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.ReadPhoneState))
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle(Resource.String.alert_title);
                    builder.SetMessage(Resource.String.read_phone_state_rationale);
                    builder.SetPositiveButton(Resource.String.text_reply, new EventHandler<DialogClickEventArgs>((sender, args) =>
                    {
                        ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadPhoneState }, RequestPermissionCode);
                    }));
                    builder.Show();
                }
                else
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadPhoneState }, RequestPermissionCode);
            }
            else
                InitData();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == RequestPermissionCode)
            {
                if (grantResults[0] == Permission.Granted)
                {
                    InitData();
                }
                else
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle(Resource.String.alert_title);
                    builder.SetMessage(Resource.String.request_permission_tips);
                    builder.SetPositiveButton(Resource.String.text_reply, new EventHandler<DialogClickEventArgs>((sender, args) =>
                    {
                        Finish();
                    }));
                    builder.Show();
                }
            }
        }

        private void InitSDK()
        {
            if (ezopenSDKInitilized) return;
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
            EZOpenSDK.InitLib(Application, AppKey, "");

            ezopenSDKInitilized = true;
        }

        private async Task InitData()
        {
            InitSDK();
            EZOpenSDK.Instance.SetAccessToken(AccessTokenForTest);
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
        private int RequestPermissionCode = 100;
    }
}
