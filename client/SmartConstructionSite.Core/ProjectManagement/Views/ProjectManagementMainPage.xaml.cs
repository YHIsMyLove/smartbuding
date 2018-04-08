using SmartConstructionSite.Core.ProjectManagement.ViewModels;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Events.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartConstructionSite.Core.PeopleManagement.Views;
using Acr.UserDialogs;
using System.Collections.Specialized;
using SmartConstructionSite.Core.Events.ViewModels;
using SmartConstructionSite.Core.Events.Models;
using SmartConstructionSite.Core.Account.Views;
using SmartConstructionSite.Core.Account.Services;

namespace SmartConstructionSite.Core.ProjectManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectManagementMainPage : ContentPage
    {
        private ProjectManagementMainViewModel viewModel;

        public ProjectManagementMainPage()
        {
            viewModel = new ProjectManagementMainViewModel();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.LatestMeetings.CollectionChanged += LatestMeetings_Changed;
            BindingContext = viewModel;
            InitializeComponent();
            //Appearing += ProjectManagementMainPage_Appearing;
        }

        private void LatestMeetings_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (viewModel.LatestMeetings.Count >= 0)
            {
                rollingBoard.HeightRequest = 40;
                rollingBoard.IsVisible = true;
                rollingBoard.Start();
            }
            else
            {
                rollingBoard.HeightRequest = 0;
                rollingBoard.IsVisible = false;
                rollingBoard.Stop();
            }
        }

        private async void ProjectManagementMainPage_Appearing(object sender, EventArgs e)
        {
            //root.Layout(Bounds);
            //if (ServiceContext.Instance.CurrentUser == null)
            //{
            //    if (Application.Current.Properties.ContainsKey("SessionID"))
            //    {
            //        string sessionId = (string)Application.Current.Properties["SessionID"];
            //        var result = await new UserService().GetUserInfo(sessionId);
            //    }
            //    else
            //    {
            //        Navigation.InsertPageBefore(new LoginPage(), this);
            //        await Task.Delay(200);
            //        await Navigation.PopAsync();
            //    }
            //}
            //else
            //{
            //    if (ServiceContext.Instance.CurrentProject == null)
            //    {
            //        await Task.Delay(200);
            //        await Navigation.PushAsync(new ProjectListPage());
            //    }
            //}
            //viewModel.ChangeProjectCommand.Execute(null);
            //viewModel.InitCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            rollingBoard.Start();
            root.Children.Remove(grid);
            root.Children.Add(grid);

            MessagingCenter.Subscribe<Application>(this, "NewMeeting", (sender) => {
                Device.BeginInvokeOnMainThread(async () => {
                    await viewModel.UpdateMessageAndRelationalCount();
                });
            });

            CheckProject();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            rollingBoard.Stop();

            MessagingCenter.Unsubscribe<Application>(this, "NewMeeting");
        }

        private async Task CheckProject()
        {
            if (ServiceContext.Instance.CurrentUser == null) return;
            if (ServiceContext.Instance.CurrentProject == null)
            {
                await Task.Delay(200);
                await Navigation.PushAsync(new ProjectListPage());
            }
            else
            {
                viewModel.ChangeProjectCommand.Execute(null);
                viewModel.InitCommand.Execute(null);
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.IsBusy))
            {
                if (viewModel.IsBusy)
                    UserDialogs.Instance.ShowLoading("请稍后。。。", MaskType.Black);
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

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (sender == btnSceneMgr)
                ((App)Application.Current).ShowCameraList();
            else if (sender == btnProductionMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "生产管理" }, true);
            else if (sender == btnSpecialTaskCheck)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "特种作业审批" }, true);
            else if (sender == btnAssetMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "资产管理" }, true);
            else if (sender == btnEvents)
                await Navigation.PushAsync(new EventListPage(), true);
            else if (sender == btnPeopleMgr)
                await Navigation.PushAsync(new PeopleManagementMainPage(), true);
            else if (sender == btnEnvMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "环境监测" }, true);
            else if (sender == btnDevMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "机械设备监测" }, true);
            else if (sender == btnProjectProgress)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "项目进度" }, true);
            else if (sender == btnChangeProject)
                await Navigation.PushAsync(new ProjectListPage());
            else if (sender == rollingBoard)
            {
                rollingBoard.HeightRequest = 0;
                rollingBoard.IsVisible = false;
            }
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (sender == rollingBoard)
            {
                await Navigation.PushAsync(new EventListPage());
                var eventDetailPage = new EventDetailPage();
                var eventDetailViewModel = new EventDetailViewModel((Meeting)rollingBoard.CurrentMessage);
                eventDetailPage.BindingContext = eventDetailViewModel;
                await Navigation.PushAsync(eventDetailPage);
            }
        }
    }
}