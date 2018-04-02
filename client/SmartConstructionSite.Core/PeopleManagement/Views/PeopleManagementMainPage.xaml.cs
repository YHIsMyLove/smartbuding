using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.PeopleManagement.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PeopleManagementMainPage : ContentPage
	{
		public PeopleManagementMainPage ()
		{
			InitializeComponent ();
		}

        async void Handle_Tapped(object sender, EventArgs args)
        {
            if (sender == btnCheckout)
            {
                //todo
            }
            else if (sender == btnContacts)
            {
                await Navigation.PushAsync(new ContactsListPage(), true);
            }
            else if (sender == btnSaftyEducation)
            {
                //todo
            }
        }
	}
}