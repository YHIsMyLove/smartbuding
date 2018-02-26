using System;
using Android.Content;
using Android.Widget;
using SmartConstructionSite.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SmartConstructionSite.Cameras.VideoView), typeof(VideoViewRenderer))]
namespace SmartConstructionSite.Droid.Renderers
{
    public class VideoViewRenderer : ViewRenderer<Cameras.VideoView, VideoView>
    {
        public VideoViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Cameras.VideoView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                VideoView videoView = new VideoView(Context);
                SetNativeControl(videoView);
            }
        }
    }
}
