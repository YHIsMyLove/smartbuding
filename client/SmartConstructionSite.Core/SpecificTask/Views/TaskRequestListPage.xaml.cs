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
    }
}
