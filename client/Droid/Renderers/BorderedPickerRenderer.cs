using System;
using Android.Content;
using SmartConstructionSite.Core.Common;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderedPicker), typeof(SmartConstructionSite.Droid.Renderers.BorderedPickerRenderer))]
namespace SmartConstructionSite.Droid.Renderers
{
    public class BorderedPickerRenderer : PickerRenderer
    {
        public BorderedPickerRenderer(Context context) : base(context)
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
            base.OnElementChanged(e);

            if (Control != null)
            {
                //var bg = Context.GetDrawable(Resource.Drawable.bordered_bg);
                //Control.Background = bg;
            }
		}
	}
}
