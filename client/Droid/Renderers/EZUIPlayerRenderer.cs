using System;
using Com.Ezvizuikit.Open;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ARelativeLayout = Android.Widget.RelativeLayout;
using SmartConstructionSite.Droid.Renderers;
using Android.Content;
using Android.Widget;
using Java.Util;
using System.ComponentModel;
using Android.Util;
using Android.Views;
using Android.Runtime;
using Android.App;

[assembly: ExportRenderer(typeof(EZUIKitForms.EZUIPlayer), typeof(EZUIPlayerRenderer))]
namespace SmartConstructionSite.Droid.Renderers
{
    public class EZUIPlayerRenderer : ViewRenderer<EZUIKitForms.EZUIPlayer, ARelativeLayout>
    {
        class MyOrientationDetector : OrientationEventListener
        {
            private EZUIPlayerRenderer owner;
            private IWindowManager mWindowManager;
            private int mLastOrientation = 0;

            public MyOrientationDetector(EZUIPlayerRenderer owner, Context context) : base(context)
            {
                this.owner = owner;
                mWindowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            }

            public bool IsWideScrren()
            {
                Display display = mWindowManager.DefaultDisplay;
                Android.Graphics.Point pt = new Android.Graphics.Point();
                display.GetSize(pt);
                return pt.X > pt.Y;
            }

            public override void OnOrientationChanged(int orientation)
            {
                Activity act = owner.Context.JavaCast<Activity>();
                int value = GetCurentOrientationEx(orientation);
                if (value != mLastOrientation)
                {
                    mLastOrientation = value;
                    var current = act.RequestedOrientation;
                    if (current == Android.Content.PM.ScreenOrientation.Portrait
                        || current == Android.Content.PM.ScreenOrientation.Landscape)
                    {
                        act.RequestedOrientation = Android.Content.PM.ScreenOrientation.Sensor;
                    }
                }
            }

            private int GetCurentOrientationEx(int orientation)
            {
                int value = 0;
                if (orientation >= 315 || orientation < 45)
                {
                    // 0度
                    value = 0;
                    return value;
                }
                if (orientation >= 45 && orientation < 135)
                {
                    // 90度
                    value = 90;
                    return value;
                }
                if (orientation >= 135 && orientation < 225)
                {
                    // 180度
                    value = 180;
                    return value;
                }
                if (orientation >= 225 && orientation < 315)
                {
                    // 270度
                    value = 270;
                    return value;
                }
                return value;
            }
        }

        class CallBack : Java.Lang.Object, EZUIPlayer.IEZUIPlayerCallBack
        {
            EZUIPlayerRenderer owner;

            public CallBack(EZUIPlayerRenderer owner)
            {
                this.owner = owner;
            }

            public void OnPlayFail(EZUIError p0)
            {
                // TODO: 2017/2/21 播放失败处理
                if (p0.ErrorString.Equals(EZUIError.UeErrorInnerVerifycodeError))
                {

                }
                else if (p0.ErrorString.Equals(EZUIError.UeErrorNotFoundRecordFiles))
                {
                    // TODO: 2017/5/12
                    //未发现录像文件
                    Toast.MakeText(owner.Context, "未找到录像文件", ToastLength.Long).Show();
                }
            }

            public void OnPlayFinish()
            {
                System.Diagnostics.Debug.Write("Video finished");
            }

            public void OnPlaySuccess()
            {
                ((EZUIKitForms.IVideoPlayerController)owner.Element).Status = EZUIKitForms.VideoStatus.Playing;
            }

            public void OnPlayTime(Calendar p0)
            {
                if (p0 != null)
                {
                    System.Diagnostics.Debug.Write("OnPlayTime.calendar = {0}", p0.Time.ToString());
                }
            }

            public void OnPrepared()
            {
                if (owner.Element.AutoPlay)
                {
                    owner.player.StartPlay();
                }
            }

            public void OnVideoSizeChange(int p0, int p1)
            {
                System.Diagnostics.Debug.Write("OnVideoSizeChanged. width = " + p0 + ", height = " + p1);
            }
        }

        public EZUIPlayerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<EZUIKitForms.EZUIPlayer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    player = new EZUIPlayer(Context);
                    //orientationDetector = new MyOrientationDetector(this, Context);
                    // Put the EZUIPlayer in a RelativeLayout
                    ARelativeLayout relativeLayout = new ARelativeLayout(Context);
                    relativeLayout.AddView(player);

                    // Center the EZUIPlayer in the RelativeLayout
                    ARelativeLayout.LayoutParams layoutParams =
                                       new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                    //layoutParams.AddRule(LayoutRules.CenterInParent);
                    player.LayoutParameters = layoutParams;

                    if (string.IsNullOrEmpty(e.NewElement.AppKey))
                    {
                        Toast.MakeText(Context, "appkey,accesstoken or playUrl is null", ToastLength.Long).Show();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(e.NewElement.GlobalAreaDomain))
                        {
                            EZUIKit.InitWithAppKey((Android.App.Application)Context.ApplicationContext, e.NewElement.AppKey);
                        }
                        else
                        {
                            EZUIKit.InitWithAppKeyGlobal((Android.App.Application)Context.ApplicationContext, e.NewElement.AppKey, e.NewElement.GlobalAreaDomain);
                        }
                    }
                    EZUIKit.SetAccessToken(e.NewElement.AccessToken);
                    callBack = new CallBack(this);
                    player.SetCallBack(callBack);
                    player.SetUrl(e.NewElement.Url);
                    SetSurfaceSize();
                    SetNativeControl(relativeLayout);
                }

                e.NewElement.UpdateStatus += OnUpdateStatus;
                e.NewElement.PlayRequested += OnPlayRequested;
                e.NewElement.PauseRequested += OnPauseRequested;
                e.NewElement.StopRequested += OnStopRequested;
                e.NewElement.ChangeSurfaceSizeRequested += OnChangeSurfaceSizeRequested;
                //orientationDetector.Enable();
                MessagingCenter.Subscribe<EZUIPlayerRenderer, Android.Content.Res.Configuration>(this, "ConfigurationChanged", (sender, arg) => {
                    SetSurfaceSize();
                });
            }

            if (e.OldElement != null)
            {
                e.OldElement.UpdateStatus -= OnUpdateStatus;
                e.OldElement.PlayRequested -= OnPlayRequested;
                e.OldElement.PauseRequested -= OnPauseRequested;
                e.OldElement.StopRequested -= OnStopRequested;
                e.NewElement.ChangeSurfaceSizeRequested -= OnChangeSurfaceSizeRequested;
                //orientationDetector.Dispose();
                MessagingCenter.Unsubscribe<EZUIPlayerRenderer, Android.Content.Res.Configuration>(this, "ConfigurationChanged");
            }
        }

        private void OnChangeSurfaceSizeRequested(object sender, EventArgs e)
        {
            SetSurfaceSize();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == EZUIKitForms.EZUIPlayer.UrlProperty.PropertyName)
            {
                player.SetUrl(Element.Url);
            }
            else if (args.PropertyName == EZUIKitForms.EZUIPlayer.PositionProperty.PropertyName)
            {
                //todo
            }
        }

        protected override void Dispose(bool disposing)
        {
            //if (orientationDetector != null)
            //{
            //    orientationDetector.Dispose();
            //}
            if (Control != null && player != null)
            {
                player.ReleasePlayer();
            }
            if (Element != null)
            {
                Element.UpdateStatus -= OnUpdateStatus;
            }

            base.Dispose(disposing);
        }

        void SetSurfaceSize()
        {
            DisplayMetrics dm = new DisplayMetrics();
            IWindowManager winMgr = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            winMgr.DefaultDisplay.GetMetrics(dm);
            player.SetSurfaceSize(dm.WidthPixels, dm.WidthPixels > dm.HeightPixels ? dm.HeightPixels : 0);
        }

        // Event handler to update status
        void OnUpdateStatus(object sender, EventArgs args)
        {
            EZUIKitForms.VideoStatus status = EZUIKitForms.VideoStatus.NotReady;

            status = player.Status == EZUIPlayer.StatusPlay ? EZUIKitForms.VideoStatus.Playing :
                           player.Status == EZUIPlayer.StatusPause ? EZUIKitForms.VideoStatus.Paused : EZUIKitForms.VideoStatus.NotReady;
            if (player.Status == EZUIPlayer.StatusPlay)
                status = EZUIKitForms.VideoStatus.Playing;
            else if (player.Status == EZUIPlayer.StatusStop)
                status = EZUIKitForms.VideoStatus.Stoped;
            else if (player.Status == EZUIPlayer.StatusPause)
                status = EZUIKitForms.VideoStatus.Paused;

            ((EZUIKitForms.IVideoPlayerController)Element).Status = status;

            ((IElementController)Element).SetValueFromRenderer(EZUIKitForms.EZUIPlayer.PositionProperty, new TimeSpan(0));
        }

        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            player.StartPlay();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            player.PausePlay();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            player.StopPlay();
        }

        private EZUIPlayer player;
        private CallBack callBack;
        //private MyOrientationDetector orientationDetector;
    }
}
