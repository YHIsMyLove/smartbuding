using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SmartConstructionServices.Account.Services;
using SmartConstructionServices.Account.ViewModels;
using SmartConstructionServices.Common;
using SmartConstructionSite.Account;
using SmartConstructionSite.ProjectManagement;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartConstructionSite
{
    public partial class MainPage : MasterDetailPage
    {
        UserMainPage userMainPage;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            userMainPage = new UserMainPage();
            userMainPage.ListView.ItemSelected += OnItemSelected;
            Master = userMainPage;
            Detail = new NavigationPage(new ProjectManagementMainPage());
            Appearing += MainPage_Appearing;
            ((UserMainViewModel)userMainPage.BindingContext).PropertyChanged += UserMainViewModel_PropertyChanged;
        }

        private async Task CheckPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        {
                            await DisplayAlert("请求权限", "智慧工地需要访问您的位置信息", "知道了");
                        }
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    //todo
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //todo
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                userMainPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }

        private async void UserMainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var viewModel = ((UserMainViewModel)Master.BindingContext);
            if (e.PropertyName == nameof(viewModel.IsLogoutSucceed) && viewModel.IsLogoutSucceed)
            {
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Task.Delay(200);
                await Navigation.PopAsync();
            }
        }

        private async void MainPage_Appearing(object sender, System.EventArgs e)
        {
            if (ServiceContext.Instance.CurrentUser == null)
            {
                if (Application.Current.Properties.ContainsKey("SessionId"))
                {
                    string sessionId = (string)Application.Current.Properties["SessionId"];
                    await CheckSession(sessionId);
                }
                else
                {
                    Navigation.InsertPageBefore(new LoginPage(), this);
                    await Task.Delay(200);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await CheckPermissions();
            }
        }

        private async Task CheckSession(string sessionId)
        {
            var result = await new UserService().CheckSession(sessionId);
            if (result.HasError)
            {
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Task.Delay(200);
                await Navigation.PopAsync(true);
            }
            else
            {
                ServiceContext.Instance.CurrentUser = result.Model;
                await CheckPermissions();
            }
        }
    }
}
