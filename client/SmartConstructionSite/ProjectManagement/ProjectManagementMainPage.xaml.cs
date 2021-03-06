﻿using SmartConstructionServices.ProjectManagement.ViewModels;
using SmartConstructionSite.AssetManagement;
using SmartConstructionSite.Common;
using SmartConstructionSite.Events;
using SmartConstructionSite.PeopleManagement;
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

            Device.StartTimer(TimeSpan.FromMilliseconds(200), Rolling);
        }

        private bool Rolling()
        {
            message.Margin = new Thickness(message.Margin.Left - 5, 0, 0, 0);
            if (message.Margin.Left <= -message.Width)
                message.Margin = new Thickness(billborard.Width, 0, 0, 0);
            return true;
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
                await Navigation.PushAsync(new EventListPage(), true);
            else if (sender == btnPeopleMgr)
                await Navigation.PushAsync(new ContactsListPage(), true);
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