using System;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SmartConstructionSite.Droid.Renderers
{
    public class BorderedEntryRenderer : EntryRenderer
    {
        public BorderedEntryRenderer(Context context) : base(context)
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
            base.OnElementChanged(e);

            if (Control != null)
            {
                var bg = Context.GetDrawable(Resource.Drawable.bordered_bg);
                Control.Background = bg;
            }
		}
	}
}
