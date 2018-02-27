using System;
using SmartConstructionSite.Cameras;
using SmartConstructionSite.iOS.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VideoView), typeof(VideoViewRenderer))]
namespace SmartConstructionSite.iOS.Renders
{
    public class VideoViewRenderer : ViewRenderer
    {
        public VideoViewRenderer()
        {
        }
    }
}
