using BottomBar.XamarinForms;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.ProjectManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace SmartConstructionSite.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : BottomBarPage
    {
		public HomePage ()
		{
			InitializeComponent ();
		}

        public ProjectManagementMainPage ProjMgtPage
        {
            get { return projMgtPage; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
	}
}