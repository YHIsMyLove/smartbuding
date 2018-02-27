using System;
using System.Collections.Generic;
using System.Windows.Input;
using SmartConstructionServices.OnlineMonitoring.ViewModels;
using Xamarin.Forms;
using EZUIKitForms;

namespace SmartConstructionSite.OnlineMonitoring
{
    public partial class CameraLiveStreamingPage : ContentPage
    {
        CameraLiveStreamingViewModel viewModel;

        public CameraLiveStreamingPage()
        {
            viewModel = new CameraLiveStreamingViewModel();
            BindingContext = viewModel;

            InitCommands();
            ((App)Application.Current).SetFullScreen(true);

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            Resources["PlayerTemplate"] = Resources["PortraitTemplate"];
        }

        public ICommand MaximizeCommand { private set; get; }

        public ICommand MinimizeCommand { private set; get; }

        public ICommand PlayCommand { private set; get; }

        public ICommand MuteCommand { private set; get; }

        public ICommand ChangeViewCommand { private set; get; }

        public ICommand ShowVideoLevelPopupCommand { private set; get; }

        public static readonly BindableProperty MaximizedProperty =
            BindableProperty.Create(nameof(Maximized), typeof(bool), typeof(CameraLiveStreamingPage), false,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((CameraLiveStreamingPage)bindable).SetLandscape((bool)newValue); });

        public bool Maximized
        {
            get { return (bool)GetValue(MaximizedProperty); }
            set { SetValue(MaximizedProperty, value); }
        }

        public EZUIPlayer Player { get { return player; } }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((App)Application.Current).SetFullScreen(false);
        }

        private void SetLandscape(bool landscape)
        {
            if (landscape)
            {
                ((App)Application.Current).SetLandscape(true);
                Resources["PlayerTemplate"] = Resources["LandscapeTemplate"];
            }
            else
            {
                ((App)Application.Current).SetLandscape(false);
                Resources["PlayerTemplate"] = Resources["PortraitTemplate"];
            }
        }

        private void InitCommands()
        {
            MaximizeCommand = new Command(
                execute: () =>
                {
                    SetLandscape(true);
                });
            MinimizeCommand = new Command(
                execute: () =>
                {
                    SetLandscape(false);
                });
            PlayCommand = new Command(
                execute: () =>
                {
                    if (player.Status == VideoStatus.Stoped)
                    {
                        player.Play();
                    }
                    else if (player.Status == VideoStatus.Playing)
                    {
                        player.Stop();
                    }
                    ((Command)PlayCommand).ChangeCanExecute();
                },
                canExecute: () =>
                {
                    return player.Status != VideoStatus.NotReady;
                });
            MuteCommand = new Command(
                execute: () =>
                {
                    //todo
                    ((Command)MuteCommand).ChangeCanExecute();
                },
                canExecute: () =>
                {
                    return player.Status != VideoStatus.NotReady;
                });
            ChangeViewCommand = new Command(
                execute: () =>
                {
                    //todo
                    ((Command)ChangeViewCommand).ChangeCanExecute();
                },
                canExecute: () =>
                {
                    return player.Status != VideoStatus.NotReady;
                });
            ShowVideoLevelPopupCommand = new Command(
                execute: () =>
                {
                    //todo
                    ((Command)ShowVideoLevelPopupCommand).ChangeCanExecute();
                },
                canExecute: () =>
                {
                    return player.Status != VideoStatus.NotReady;
                });
        }
    }
}
