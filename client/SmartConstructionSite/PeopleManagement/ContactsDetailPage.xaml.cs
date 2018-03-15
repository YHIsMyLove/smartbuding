using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.PeopleManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsDetailPage : ContentPage
    {
        public ContactsDetailPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //ContactsDetailViewModel viewModel = BindingContext as ContactsDetailViewModel;
            //if (viewModel == null || viewModel.Contacts == null || string.IsNullOrEmpty(viewModel.Contacts.PhoneNumber)) return;
            //var phoneDialer = CrossMessaging.Current.PhoneDialer;
            //if (phoneDialer.CanMakePhoneCall)
            //    phoneDialer.MakePhoneCall(viewModel.Contacts.PhoneNumber);
        }
    }
}