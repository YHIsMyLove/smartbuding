using System;
using System.Collections.Generic;
using SmartConstructionSite.AssetManagement;
using SmartConstructionSite.Events;
using SmartConstructionSite.OnlineMonitoring;
using SmartConstructionSite.ProductionManagement;
using SmartConstructionSite.SpecialTaskCheck;
using Xamarin.Forms;

namespace SmartConstructionSite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (sender == btnSceneMgr)
                ((App)Application.Current).CameraHelper.ShowCameraList();
            else if (sender == btnProductionMgr)
                await Navigation.PushAsync(new ProductionManagementMainPage(), true);
            else if (sender == btnSpecialTaskCheck)
                await Navigation.PushAsync(new SpecialTaskCheckMainPage(), true);
            else if (sender == btnAssetMgr)
                await Navigation.PushAsync(new AssetListPage(), true);
            else if (sender == btnEvents)
                await Navigation.PushAsync(new EventsMainPage(), true);
            else if (sender == barItemSearch)
            {
                
            }
        }

    }
}
