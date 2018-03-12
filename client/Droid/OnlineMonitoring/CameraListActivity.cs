
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
using Plugin.Permissions;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    [Activity(Label = "@string/title_dev_list")]
    public class CameraListActivity : Activity
    {
        public const string AppKey = "443eed7d6dab47739915d6a237dcad34";
        public const string AccessTokenForTest = "at.5dx4wfc728ucu7zj7hk1buzz7m18fwzu-707o7jsvz2-12g99x3-aruh9vwpv";
        static bool ezopenSDKInitilized;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_camera_list);
            InitViews();
            //InitData();
            CheckPermissions();
        }

        private async Task CheckPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Phone);
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Phone))
                        {
                            AlertDialog.Builder builder = new AlertDialog.Builder(this);
                            builder.SetTitle("请求权限");
                            builder.SetMessage("智慧工地需要读取您手机的状态来更好的运行");
                            builder.Show();
                        }
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Phone);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Phone))
                        status = results[Plugin.Permissions.Abstractions.Permission.Phone];
                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    await InitData();
                }
                else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle(Resource.Id.alertTitle);
                    builder.SetMessage("如果禁用该权限，该功能将不能使用，你确定禁用该权限吗？");
                    builder.SetPositiveButton("确定", new EventHandler<DialogClickEventArgs>((sender, args) => {
                        Finish();
                    }));
                    builder.SetNegativeButton("重新赋予", new EventHandler<DialogClickEventArgs>(async (sender, args) => {
                        await CheckPermissions();
                    }));
                    builder.Show();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
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
    }
}
