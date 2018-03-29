using BottomBar.XamarinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : BottomBarPage
    {
		public HomePage ()
		{
			InitializeComponent ();
		}
	}
}