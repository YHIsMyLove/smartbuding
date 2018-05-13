using System;
using System.Collections.Generic;
using Android.Content;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Data;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SmartConstructionSite.Core.Common.PieChart), typeof(SmartConstructionSite.Droid.Renderers.PieChartRenderer))]
namespace SmartConstructionSite.Droid.Renderers
{
	public class PieChartRenderer : ViewRenderer<Core.Common.PieChart, PieChart>
    {
		public PieChartRenderer(Context context) : base(context)
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Core.Common.PieChart> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				var chart = new PieChart(Context);
				var entries = new List<PieEntry>();
				entries.Add(new PieEntry(90));
				entries.Add(new PieEntry(10));
				var dataSet = new PieDataSet(entries, "Label");
				var pieData = new PieData(dataSet);
				chart.Data = pieData;
				chart.Invalidate();
				SetNativeControl(chart);
			}
		}
	}
}
