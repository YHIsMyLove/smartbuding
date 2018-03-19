
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Ezvizuikit.Open;
using Com.Videogo.Constant;
using Com.Videogo.Openapi.Bean;
using Com.Videogo.Realplay;
using Java.Util;
using Com.Videogo.Openapi;
using Com.Videogo.Util;
using Android.Content.PM;
using System.Threading.Tasks;
using Com.Videogo.Widget;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    [Activity(Label = "CameraLiveStreamingActivity", Theme = "@style/MyTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class CameraLiveStreamingActivity : Activity
    {
        class PlayerCallBack : Java.Lang.Object, EZUIPlayer.IEZUIPlayerCallBack
        {
            CameraLiveStreamingActivity owner;

            public PlayerCallBack(CameraLiveStreamingActivity owner)
            {
                this.owner = owner;
            }

            public void OnPlayFail(EZUIError p0)
            {
                Toast.MakeText(owner, "播放错误。错误描述：" + p0.ErrorString, ToastLength.Long).Show();
                owner.UpdateViewOnStateChanged();
            }

            public void OnPlayFinish()
            {
                Log.Debug(tag, "OnPlaySuccess");
            }

            public void OnPlaySuccess()
            {
                Log.Debug(tag, "OnPlaySuccess");
                owner.UpdateViewOnStateChanged();
            }

            public void OnPlayTime(Calendar p0)
            {
                Log.Debug(tag, "OnPlayTime.time: {0}", p0.Time.ToString());
            }

            public void OnPrepared()
            {
                Log.Debug(tag, "OnPrepared");
                owner.player.StartPlay();
                owner.UpdateViewOnStateChanged();
            }

            public void OnVideoSizeChange(int p0, int p1)
            {
                Log.Debug(tag, "OnVideoSizeChange.new size x: {0}, y: {1}", p0, p1);
                owner.UpdatePlayerSurfaceSize();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_camera_live_streaming);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            device = (EZDeviceInfo)Intent.GetParcelableExtra(IntentConsts.ExtraDeviceInfo);
            camera = (EZCameraInfo)Intent.GetParcelableExtra(IntentConsts.ExtraCameraInfo);
            Log.Debug(Tag, string.Format("CameraLiveStreamingActivity#OnCreate.\n\tDevice: {0}, \n\tCamera: {1}", device, camera));

            InitView();
        }

        protected override void OnPause()
        {
            base.OnPause();
            player.PausePlay();
        }

        protected override void OnResume()
        {
            base.OnResume();
            player.StartPlay();
        }

        public override void OnBackPressed()
        {
            if (maximized)
                Maximize();
            else
                base.OnBackPressed();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            player.ReleasePlayer();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            UpdatePlayerSurfaceSize();
        }

        private void InitView()
        {
            //player
            player = (EZUIPlayer)FindViewById(Resource.Id.player);
            EZUIKit.InitWithAppKey(Application, CameraListActivity.AppKey);
            EZUIKit.SetAccessToken(CameraListActivity.AccessTokenForTest);
            player.SetCallBack(new PlayerCallBack(this));
            string url = string.Format("ezopen://open.ys7.com/{0}/{1}.live", camera.DeviceSerial, camera.CameraNo);
            player.SetUrl(url);
            UpdatePlayerSurfaceSize();

            //player control
            btnPlay = FindViewById<ImageButton>(Resource.Id.btnPlay);
            btnPlay.Click += (sender, e) => Play();
            btnMute = FindViewById<ImageButton>(Resource.Id.btnMute);
            btnPlay.Click += (sender, e) => Mute();
            btnVideoLevel = FindViewById<Button>(Resource.Id.btnVideoLevel);
            btnVideoLevel.Click += (sender, e) => ShowVideoLevelPopup(btnVideoLevel);
            btnMaximize = FindViewById<ImageButton>(Resource.Id.btnMaximize);
            btnMaximize.Click += (sender, e) => Maximize();

            //camera ctrl
            btnTalkback = FindViewById<ImageButton>(Resource.Id.btnTalkback);
            btnTalkback.Enabled = device.IsSupportTalk() != EZConstants.EZTalkbackCapability.EZTalkbackNoSupport;
            btnTalkback.Click += (sender, e) => ShowTalkbackWindow();

            btnCtrl = FindViewById<ImageButton>(Resource.Id.btnCtrl);
            btnCtrl.Enabled = device.IsSupportPTZ;
            btnCtrl.Click += (sender, e) => ShowCtrlWindow();

            btnCapture = FindViewById<ImageButton>(Resource.Id.btnCapture);
            btnCapture.Click += (sender, e) => DoCapture();

            btnRecord = FindViewById<ImageButton>(Resource.Id.btnRecord);
            btnRecord.Click += (sender, e) => DoRecord();

            View container = FindViewById(Resource.Id.viewCtrlToolsContainer);
            container.Enabled = device.IsSupportPTZ;

            ImageButton btnUp = FindViewById<ImageButton>(Resource.Id.btnUp);
            btnUp.Touch += async (sender, e) =>
            {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandUp, e.Event.Action, container);
            };

            ImageButton btnDown = FindViewById<ImageButton>(Resource.Id.btnDown);
            btnDown.Touch += async (sender, e) =>
            {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandDown, e.Event.Action, container);
            };

            ImageButton btnLeft = FindViewById<ImageButton>(Resource.Id.btnLeft);
            btnLeft.Touch += async (sender, e) =>
            {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandLeft, e.Event.Action, container);
            };

            ImageButton btnRight = FindViewById<ImageButton>(Resource.Id.btnRight);
            btnRight.Touch += async (sender, e) =>
            {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandRight, e.Event.Action, container);
            };

            viewToolsNormalState = FindViewById(Resource.Id.viewToolsNormalState);
        }

        /// <summary>
        /// Record images of current camera.
        /// </summary>
        private void DoRecord()
        {
            //todo
        }

        /// <summary>
        /// Capture the image of current camera.
        /// </summary>
        private void DoCapture()
        {
            //todo
        }

        /// <summary>
        /// Shows the talkback window.
        /// </summary>
        private void ShowTalkbackWindow()
        {
            LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.camera_talkback, null, true);
            int height = FindViewById(Resource.Id.viewCameraCtrlContainer).Height;
            PopupWindow window = new PopupWindow(view, ViewGroup.LayoutParams.MatchParent, height, true);
            window.ShowAsDropDown((View)btnPlay.Parent);

            ImageButton btnClose = view.FindViewById<ImageButton>(Resource.Id.btnClose);
            btnClose.Touch += (sender, e) => window.Dismiss();

            RingView ringView = view.FindViewById<RingView>(Resource.Id.ringView);
            ringView.Visibility = ViewStates.Invisible;

            Button btnTalk = view.FindViewById<Button>(Resource.Id.btnTalkback);
            btnTalk.Touch += (sender, e) => {
                switch (e.Event.Action)
                {
                    case MotionEventActions.Down:
                        ringView.Visibility = ViewStates.Visible;
                        break;
                    case MotionEventActions.Up:
                        ringView.Visibility = ViewStates.Invisible;
                        break;
                }
            };

            window.Update();
            ringView.Post(()=>{
                ringView.SetMinRadiusAndDistance(btnTalk.Height / 2.0f, Utils.Dip2px(this, 22));
            });
        }

        /// <summary>
        /// Shows the control window.
        /// </summary>
        private void ShowCtrlWindow()
        {
            LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.camera_ctrl, null, true);
            int height = FindViewById(Resource.Id.viewCameraCtrlContainer).Height;
            PopupWindow window = new PopupWindow(view, ViewGroup.LayoutParams.MatchParent, height, true);
            window.ShowAsDropDown((View)btnPlay.Parent);

            View container = view.FindViewById(Resource.Id.viewCtrlToolsContainer);

            ImageButton btnClose = view.FindViewById<ImageButton>(Resource.Id.btnClose);
            btnClose.Touch += (sender, e) => window.Dismiss();

            ImageButton btnUp = view.FindViewById<ImageButton>(Resource.Id.btnUp);
            btnUp.Touch += async (sender, e) => {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandUp, e.Event.Action, container);
            };

            ImageButton btnDown = view.FindViewById<ImageButton>(Resource.Id.btnDown);
            btnDown.Touch += async (sender, e) => {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandDown, e.Event.Action, container);
            };

            ImageButton btnLeft = view.FindViewById<ImageButton>(Resource.Id.btnLeft);
            btnLeft.Touch += async (sender, e) => {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandLeft, e.Event.Action, container);
            };

            ImageButton btnRight = view.FindViewById<ImageButton>(Resource.Id.btnRight);
            btnRight.Touch += async (sender, e) => {
                await DoCtrl(EZConstants.EZPTZCommand.EZPTZCommandRight, e.Event.Action, container);
            };
        }

        /// <summary>
        /// Sends the command.
        /// </summary>
        /// <param name="cmd">the command</param>
        /// <param name="action">Action.</param>
        private async Task DoCtrl(EZConstants.EZPTZCommand cmd, MotionEventActions action, View container)
        {
            EZConstants.EZPTZAction ptzAction = EZConstants.EZPTZAction.EZPTZActionSTOP;
            if (action == MotionEventActions.Down)
            {
                if (cmd == EZConstants.EZPTZCommand.EZPTZCommandDown)
                    container.SetBackgroundResource(Resource.Drawable.ptz_bottom_sel);
                else if (cmd == EZConstants.EZPTZCommand.EZPTZCommandLeft)
                    container.SetBackgroundResource(Resource.Drawable.ptz_left_sel);
                else if (cmd == EZConstants.EZPTZCommand.EZPTZCommandRight)
                    container.SetBackgroundResource(Resource.Drawable.ptz_right_sel);
                else if (cmd == EZConstants.EZPTZCommand.EZPTZCommandUp)
                    container.SetBackgroundResource(Resource.Drawable.ptz_up_sel);
                if (ctrlCompleted)
                {
                    ctrlCompleted = false;
                    await CameraHelpers.ControlPTZ(camera.DeviceSerial, camera.CameraNo, cmd, EZConstants.EZPTZAction.EZPTZActionSTART, EZConstants.PtzSpeedDefault);
                    ctrlCompleted = true;
                }
            }
            else if (action == MotionEventActions.Up)
            {
                container.SetBackgroundResource(Resource.Drawable.ptz_bg);
                await CameraHelpers.ControlPTZ(camera.DeviceSerial, camera.CameraNo, cmd, EZConstants.EZPTZAction.EZPTZActionSTOP, EZConstants.PtzSpeedDefault);
            }
        }

        private void Maximize()
        {
            if (maximized)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
                maximized = false;
                viewToolsNormalState.Visibility = ViewStates.Visible;
            }
            else
            {
                RequestedOrientation = ScreenOrientation.Landscape;
                maximized = true;
                viewToolsNormalState.Visibility = ViewStates.Gone;
            }
            UpdatePlayerSurfaceSize();
        }

        private void ShowVideoLevelPopup(View anchor)
        {
            LayoutInflater layoutInflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
            ViewGroup layoutView = (ViewGroup)layoutInflater.Inflate(Resource.Layout.real_play_quality_items, null, true);

            int height = 105;
            height = Utils.Dip2px(this, height);
            PopupWindow windowQuality = new PopupWindow(layoutView, ViewGroup.LayoutParams.WrapContent, height, true);
            
            Button btnQualityHigh = layoutView.FindViewById<Button>(Resource.Id.btnQualityHigh);
            btnQualityHigh.Enabled = camera.VideoLevel != EZConstants.EZVideoLevel.VideoLevelHd;
            btnQualityHigh.Click += (sender, args) => SetQuality(windowQuality, EZConstants.EZVideoLevel.VideoLevelHd);

            Button btnQualityBanlance = layoutView.FindViewById<Button>(Resource.Id.btnBalance);
            btnQualityBanlance.Enabled = camera.VideoLevel != EZConstants.EZVideoLevel.VideoLevelBalanced;
            btnQualityBanlance.Click += (sender, args) => SetQuality(windowQuality, EZConstants.EZVideoLevel.VideoLevelBalanced);

            Button btnQualityFlunet = layoutView.FindViewById<Button>(Resource.Id.btnQualityFlunet);
            btnQualityFlunet.Enabled = camera.VideoLevel != EZConstants.EZVideoLevel.VideoLevelFlunet;
            btnQualityFlunet.Click += (sender, args) => SetQuality(windowQuality, EZConstants.EZVideoLevel.VideoLevelFlunet);

            windowQuality.ShowAsDropDown(anchor, -Utils.Dip2px(this, 5), -(height + anchor.Height + Utils.Dip2px(this, 8)));
        }

        private async void SetQuality(PopupWindow window, EZConstants.EZVideoLevel level)
        {
            window.Dismiss();
            if (!ConnectionDetector.IsNetworkAvailable(this))
            {
                // 提示没有连接网络
                Utils.ShowToast(this, Resource.String.realplay_set_fail_network);
                return;
            }
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetCancelable(false);
            builder.SetMessage(Resource.String.setting_video_level);
            Android.Support.V7.App.AlertDialog dialog = builder.Show();
            bool result = await CameraHelpers.SetVideoLevel(camera.DeviceSerial, camera.CameraNo, level);
            dialog.Dismiss();

            if (result)
            {
                camera.SetVideoLevel(videoLevel.Ordinal());
                if (level == EZConstants.EZVideoLevel.VideoLevelHd)
                {
                    btnVideoLevel.Text = GetString(Resource.String.quality_hd);
                }
                else if (level == EZConstants.EZVideoLevel.VideoLevelBalanced)
                {
                    btnVideoLevel.Text = GetString(Resource.String.quality_balanced);
                }
                else if (level == EZConstants.EZVideoLevel.VideoLevelFlunet)
                {
                    btnVideoLevel.Text = GetString(Resource.String.quality_flunet);
                }
                else
                    btnVideoLevel.Text = GetString(Resource.String.quality_flunet);
            }

            player.StopPlay();
            SystemClock.Sleep(500);
            player.StartPlay();
        }

        private void Mute()
        {
            
        }

        private void UpdateViewOnStateChanged()
        {
            if (player.Status == RealPlayStatus.StatusPause)
            {
                btnPlay.SetImageResource(Resource.Drawable.play_play_selector);
            }
            else if (player.Status == RealPlayStatus.StatusPlay)
            {
                btnPlay.SetImageResource(Resource.Drawable.pause);
            }
        }

        private void Play()
        {
            if (player.Status == RealPlayStatus.StatusStop)
            {
                player.StartPlay();

            }
            else if (player.Status == RealPlayStatus.StatusPlay)
            {
                player.StopPlay();
                UpdateViewOnStateChanged();
            }
        }

        void UpdatePlayerSurfaceSize()
        {
            DisplayMetrics dm = new DisplayMetrics();
            IWindowManager winMgr = Window.WindowManager;
            winMgr.DefaultDisplay.GetMetrics(dm);
            player.SetSurfaceSize(dm.WidthPixels, dm.WidthPixels > dm.HeightPixels ? dm.HeightPixels : 0);
        }

        private static readonly string tag = typeof(CameraLiveStreamingActivity).Name;
        private EZUIPlayer player;
        private EZDeviceInfo device;
        private EZCameraInfo camera;
        private EZConstants.EZVideoLevel videoLevel = EZConstants.EZVideoLevel.VideoLevelFlunet;
        private readonly string Tag = typeof(CameraLiveStreamingActivity).Name;
        private bool maximized;
        private ImageButton btnPlay;
        private ImageButton btnMute;
        private Button btnVideoLevel;
        private ImageButton btnMaximize;
        private View viewToolsNormalState;
        private ImageButton btnTalkback;
        private ImageButton btnCtrl;
        private ImageButton btnCapture;
        private ImageButton btnRecord;
        private bool ctrlCompleted = true;
    }
}
