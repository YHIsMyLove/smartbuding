using SmartConstructionSite.Core.PeopleManagement.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.PeopleManagement.Views
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
            ContactsDetailViewModel viewModel = BindingContext as ContactsDetailViewModel;
            viewModel.PhoneCallCommand.Execute(null);
        }
    }
}