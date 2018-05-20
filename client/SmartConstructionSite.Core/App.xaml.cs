using System;
using SmartConstructionSite.Core.Common;
using Xamarin.Forms;

namespace SmartConstructionSite.Core
{
    public partial class App : Application
    {
        public event EventHandler<bool> FullScreenRequested;
        public event EventHandler<bool> LandscapeRequested;
        public event EventHandler ShowCameraListRequested;

        public App()
        {
            InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
            //MainPage = new ContentPage();
        }

        //public CameraHelper CameraHelper
        //{
        //    get
        //    {
        //        if (cameraHelper == null)
        //            cameraHelper = new CameraHelper();
        //        return cameraHelper;
        //    }
        //}

        public void SetFullScreen(bool fullScreen)
        {
            if (this.fullScreen != fullScreen)
            {
                FullScreenRequested?.Invoke(this, fullScreen);
                this.fullScreen = fullScreen;
            }
        }

        public async void GotoMeetingListPage()
		{
            if (ServiceContext.Instance.CurrentUser == null || ServiceContext.Instance.CurrentProject == null) return;
            await ((NavigationPage)MainPage).Navigation.PopToRootAsync();
            await ((NavigationPage)MainPage).Navigation.PushAsync(new Events.Views.EventListPage());
		}

		public void SetLandscape(bool landscape)
        {
            if (this.landscape != landscape)
            {
                LandscapeRequested?.Invoke(this, landscape);
                this.landscape = landscape;
            }
        }

        public void ShowCameraList()
        {
            ShowCameraListRequested?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnStart()
        {
            // Handle when your app starts


        }

        protected override void OnSleep()
        {
            Application.Current.SavePropertiesAsync();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private bool fullScreen;
        private bool landscape;
        //private CameraHelper cameraHelper;
    }
}
