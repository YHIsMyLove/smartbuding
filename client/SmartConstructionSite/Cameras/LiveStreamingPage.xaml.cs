using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SmartConstructionSite.Cameras
{
    public partial class LiveStreamingPage : ContentPage
    {
        public LiveStreamingPage()
        {
            InitializeComponent();

            videoPlayer.FullScreenStatusChanged += VideoPlayer_FullScreenStatusChanged;
        }

        private void VideoPlayer_FullScreenStatusChanged(object sender, bool value)
        {
            NavigationPage.SetHasNavigationBar(this, !value);
        }
    }
}
