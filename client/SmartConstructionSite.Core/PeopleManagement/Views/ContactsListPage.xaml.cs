using Acr.UserDialogs;
using SmartConstructionSite.Core.Account.Models;
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
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            BindingContext = viewModel;
            InitializeComponent();
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.IsBusy))
            {
                if (viewModel.IsBusy)
                    UserDialogs.Instance.ShowLoading("正在登陆。。。", MaskType.Black);
                else
                    UserDialogs.Instance.HideLoading();
            }
            else if (e.PropertyName == nameof(viewModel.Error) && viewModel.Error != null)
            {
                var toastConfig = new ToastConfig(viewModel.Error.Description);
                toastConfig.SetDuration(3000);
                UserDialogs.Instance.Toast(toastConfig);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ContactsDetailViewModel detailViewModel = new ContactsDetailViewModel();
            detailViewModel.Contacts = (User)e.Item;
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