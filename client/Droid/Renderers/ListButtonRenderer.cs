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
using SmartConstructionSite.Core.Common;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListButton), typeof(SmartConstructionSite.Droid.Renderers.ListButtonRenderer))]
namespace SmartConstructionSite.Droid.Renderers
{
    public class ListButtonRenderer : ButtonRenderer
    {
        public ListButtonRenderer(Context context):base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = null;
                Control.Focusable = true;
                Control.RequestFocus();
            }
        }
    }
}