using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BottomBar.XamarinForms;
using SmartConstructionSite.Core.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.PeopleManagement.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PeopleManagementMainPage : BottomBarPage
	{
		public PeopleManagementMainPage ()
		{
			InitializeComponent ();
		}

        async void Handle_Tapped(object sender, EventArgs args)
        {
            //if (sender == btnCheckout)
            //{
            //    await Navigation.PushAsync(new PlaceholderPage() { Title = "人员核验" });
            //}
            //else if (sender == btnContacts)
            //{
            //    await Navigation.PushAsync(new ContactsListPage(), true);
            //}
            //else if (sender == btnSaftyEducation)
            //{
            //    await Navigation.PushAsync(new PlaceholderPage() { Title = "安全教育" });
            //}
        }
	}
}