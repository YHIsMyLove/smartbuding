using System;
using System.Collections.Generic;
using SmartConstructionSite.Core.SpecificTask.ViewModels;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.SpecificTask.Views
{
    public partial class TaskRequestListPage : ContentPage
    {
        TaskRequestListViewModel viewModel;

        public TaskRequestListPage()
        {
            viewModel = new TaskRequestListViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var buttons = new string[] {
                "申请动火作业审批",
                "申请受限作业审批",
                "申请起重吊装作业审批",
                "申请高处作业审批",
                "申请脚手架拆卸作业审批",
                "申请安全防护拆除作业审批"
            };
            var button = await DisplayActionSheet("创建", null, null, buttons);
            if (string.IsNullOrEmpty(button)) return;
            if (button == buttons[0])
            {
                await Navigation.PushAsync(new DongHuoRequestPage(), true);
            }
            else
            {
                await DisplayAlert("提示", "定制开发内容", null, "知道了");
            }
        }
    }
}
