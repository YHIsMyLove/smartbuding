using SmartConstructionSite.Core.PeopleManagement.Models;
using SmartConstructionSite.Core.PeopleManagement.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.PeopleManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsListPage : ContentPage
    {
        ContactsListViewModel viewModel;
        public ContactsListPage()
        {
            viewModel = new ContactsListViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ContactsDetailViewModel detailViewModel = new ContactsDetailViewModel();
            detailViewModel.Contacts = (Contacts)e.Item;
            ContactsDetailPage detailPage = new ContactsDetailPage();
            detailPage.BindingContext = detailViewModel;
            await Navigation.PushAsync(detailPage);
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }

        private void pickerProjects_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //if ("无".Equals(pickerProjects.SelectedItem))
            //    pickerDepartments.IsEnabled = false;
            //else
                //pickerDepartments.IsEnabled = true;
        }
    }
}