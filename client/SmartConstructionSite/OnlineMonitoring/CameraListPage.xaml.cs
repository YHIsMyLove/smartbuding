using System;
using System.Collections.Generic;
using SmartConstructionServices.Common;
using SmartConstructionServices.OnlineMonitoring;
using SmartConstructionServices.OnlineMonitoring.ViewModels;
using Xamarin.Forms;

namespace SmartConstructionSite.OnlineMonitoring
{
    public partial class CameraListPage : ContentPage
    {
        
        public CameraListPage()
        {
            InitializeComponent();
            viewModel = new CameraListViewModel();
            BindingContext = viewModel;
            ServiceContext.Instance.Login("admin", "admin");
        }

        void Handle_Appearing(object sender, System.EventArgs e)
        {
            viewModel.FetchCameras();
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Item tapped.{0}", e.Item);

            var liveStreamingPage = new CameraLiveStreamingPage();
            var liveStreamingViewModel = new CameraLiveStreamingViewModel();
            liveStreamingPage.BindingContext = liveStreamingViewModel;
            liveStreamingViewModel.Camera = (Camera)e.Item;
            await Navigation.PushAsync(liveStreamingPage);
        }

        private CameraListViewModel viewModel;
    }
}
