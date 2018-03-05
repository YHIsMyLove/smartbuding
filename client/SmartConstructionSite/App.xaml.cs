using System;
using SmartConstructionSite.OnlineMonitoring;
using Xamarin.Forms;

namespace SmartConstructionSite
{
    public partial class App : Application
    {
        public event EventHandler<bool> FullScreenRequested;
        public event EventHandler<bool> LandscapeRequested;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public CameraHelper CameraHelper
        {
            get
            {
                if (cameraHelper == null)
                    cameraHelper = new CameraHelper();
                return cameraHelper;
            }
        }

        public void SetFullScreen(bool fullScreen)
        {
            if (this.fullScreen != fullScreen)
            {
                FullScreenRequested?.Invoke(this, fullScreen);
                this.fullScreen = fullScreen;
            }
        }

        public void SetLandscape(bool landscape)
        {
            if (this.landscape != landscape)
            {
                LandscapeRequested?.Invoke(this, landscape);
                this.landscape = landscape;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private bool fullScreen;
        private bool landscape;
        private CameraHelper cameraHelper;
    }
}
