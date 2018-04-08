using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SmartConstructionSite.Core.Account.ViewModels;
using SmartConstructionSite.Core.Account.Services;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Account.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using SmartConstructionSite.Core.ProjectManagement.Views;
using Acr.UserDialogs;
using SmartConstructionSite.Core.ProjectManagement.ViewModels;

namespace SmartConstructionSite.Core
{
    public partial class MainPage : MasterDetailPage
    {
        UserMainPage userMainPage;
        HomePage homePage;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            userMainPage = new UserMainPage();
            userMainPage.ListView.ItemSelected += OnItemSelected;
            Master = userMainPage;
            homePage = new HomePage();
            Detail = new NavigationPage(homePage);
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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                userMainPage.ListView.SelectedItem = null;
                IsPresented = false;
                await Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
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
                if (Application.Current.Properties.ContainsKey("SessionID"))
                {
                    string sessionId = (string)Application.Current.Properties["SessionID"];
                    var result = await new UserService().GetUserInfo(sessionId);
                    if (result.HasError)
                    {
                        var toastConfig = new ToastConfig(result.Error.Description);
                        toastConfig.SetDuration(3000);
                        UserDialogs.Instance.Toast(toastConfig);
                        await Task.Delay(1000);
                        await GotoLoginPage();
                    }
                    else
                    {
                        ServiceContext.Instance.CurrentUser = result.Model;
                        ((UserMainViewModel)userMainPage.BindingContext).User = result.Model;
                        await CheckProject();
                    }
                }
                else
                {
                    await Task.Delay(1000);
                    await GotoLoginPage();
                }
            }
            else
            {
                //await CheckPermissions();
                //await CheckProject();
            }
        }

        private async Task CheckProject()
        {
            if (ServiceContext.Instance.CurrentProject == null)
            {
                await Task.Delay(200);
                await Navigation.PushAsync(new ProjectListPage());
            }
            else
            {
                ((ProjectManagementMainViewModel)homePage.ProjMgtPage.BindingContext).ChangeProjectCommand.Execute(null);
                ((ProjectManagementMainViewModel)homePage.ProjMgtPage.BindingContext).InitCommand.Execute(null);
            }
        }

        private async Task GotoLoginPage()
        {
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Task.Delay(200);
            await Navigation.PopAsync();
        }
    }
}
