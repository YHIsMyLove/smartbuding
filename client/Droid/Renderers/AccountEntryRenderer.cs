using System;
using Android.Content;
using SmartConstructionSite.Core.Common;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AccountEntry), typeof(SmartConstructionSite.Droid.Renderers.AccountEntryRenderer))]
namespace SmartConstructionSite.Droid.Renderers
{
    public class AccountEntryRenderer : EntryRenderer
    {
        public AccountEntryRenderer(Context context):base(context)
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = null;

                var left = Context.GetDrawable(Element.IsPassword ? Resource.Drawable.ic_lock : Resource.Drawable.ic_user);
                var bottom = Context.GetDrawable(Resource.Drawable.line);
                Control.SetCompoundDrawablesWithIntrinsicBounds(left, Control.GetCompoundDrawables()[1], Control.GetCompoundDrawables()[2], bottom);
            }
		}
	}
}
