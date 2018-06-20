using Naxam.Controls.Forms;
using Plugin.Messaging;
using SmartConstructionSite.Core.Account.Models;
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

		protected override void OnAppearing()
		{
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey("SelectedContacts"))
            {
                User contacts = (User)Application.Current.Properties["SelectedContacts"];
                Application.Current.Properties.Remove("SelectedContacts");
                var smsMessenger = CrossMessaging.Current.SmsMessenger;
                if (smsMessenger.CanSendSms)
                {
                    ContactsDetailViewModel viewModel = (ContactsDetailViewModel)BindingContext;
                    string msg = $"姓名：{viewModel.Contacts.UserName}\n" +
                        $"电话：{viewModel.Contacts.UserPhoneNum}\n" +
                        $"邮箱：{viewModel.Contacts.UserEmail}";
                    smsMessenger.SendSms(contacts.UserPhoneNum, msg);
                }
            }
		}

		private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender == btnCall)
            {
                ContactsDetailViewModel viewModel = BindingContext as ContactsDetailViewModel;
                viewModel.PhoneCallCommand.Execute(null);
            }
            else if (sender == btnSend)
            {
                if (Application.Current.Properties.ContainsKey("SelectedContacts"))
                    Application.Current.Properties.Remove("SelectedContacts");
                ContactsListPage contactsListPage = new ContactsListPage();
                contactsListPage.Title = "选择一个联系人";
                contactsListPage.SelectContacts = true;
                await Navigation.PushAsync(contactsListPage, true);
            }
			else if (sender == btnLocation)
			{
				//todo
			}
        }
    }
}