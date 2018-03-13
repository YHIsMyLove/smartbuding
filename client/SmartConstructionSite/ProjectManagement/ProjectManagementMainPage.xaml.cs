using SmartConstructionServices.ProjectManagement.ViewModels;
using SmartConstructionSite.AssetManagement;
using SmartConstructionSite.Common;
using SmartConstructionSite.Events;
using SmartConstructionSite.ProductionManagement;
using SmartConstructionSite.SpecialTaskCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.ProjectManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectManagementMainPage : ContentPage
    {
        private ProjectManagementMainViewModel viewModel;

        public ProjectManagementMainPage()
        {
            viewModel = new ProjectManagementMainViewModel();
            BindingContext = viewModel;
            InitializeComponent();
            //Button.ButtonContentLayout contentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Top, 5);
            //btnDevMgr.ContentLayout = contentLayout;
            //btnEnvMgr.ContentLayout = contentLayout;
            //btnEvents.ContentLayout = contentLayout;
            //btnAssetMgr.ContentLayout = contentLayout;
            //btnSceneMgr.ContentLayout = contentLayout;
            //btnPeopleMgr.ContentLayout = contentLayout;
            //btnProductionMgr.ContentLayout = contentLayout;
            //btnProjectProgress.ContentLayout = contentLayout;
            //btnSpecialTaskCheck.ContentLayout = contentLayout;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (sender == btnSceneMgr)
                ((App)Application.Current).CameraHelper.ShowCameraList();
            else if (sender == btnProductionMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "生产管理" }, true);
            else if (sender == btnSpecialTaskCheck)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "特种作业审批" }, true);
            else if (sender == btnAssetMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "资产管理" }, true);
            else if (sender == btnEvents)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "日历事件" }, true);
            else if (sender == btnPeopleMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "人员监管" }, true);
            else if (sender == btnEnvMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "环境监测" }, true);
            else if (sender == btnDevMgr)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "机械设备监测" }, true);
            else if (sender == btnProjectProgress)
                await Navigation.PushAsync(new PlaceholderPage() { Title = "项目进度" }, true);
            else if (sender == btnChangeProject)
                await Navigation.PushAsync(new ProjectListPage());
        }
    }
}