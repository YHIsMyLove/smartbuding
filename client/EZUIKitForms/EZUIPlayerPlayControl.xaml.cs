using System;
using System.Collections.Generic;
using EZOpenSDKForms;
using Xamarin.Forms;

namespace EZUIKitForms
{
    public partial class EZUIPlayerPlayControl : ContentView
    {
        public EZUIPlayerPlayControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty PlayerProperty =
            BindableProperty.Create(nameof(Player), typeof(EZUIPlayer), typeof(EZUIPlayerPlayControl), null);

        public EZUIPlayer Player
        {
            get { return (EZUIPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public static readonly BindableProperty VideoLevelProperty =
            BindableProperty.Create(nameof(VideoLevel), typeof(EZVideoLevel), typeof(EZUIPlayerPlayControl), EZVideoLevel.Balanced);

        public EZVideoLevel VideoLevel
        {
            get { return (EZVideoLevel)GetValue(VideoLevelProperty); }
            set { SetValue(VideoLevelProperty, value); }
        }

        public static readonly BindableProperty MaximizedProperty =
            BindableProperty.Create(nameof(Maximized), typeof(bool), typeof(EZUIPlayerPlayControl), false);

        public bool Maximized
        {
            get;
            set;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (sender == btnMute)
            {
                
            }
            else if (sender == btnPlay)
            {
                
            }
            else if (sender == btnChangeView)
            {
                
            }
            else if (sender == btnFullScreen)
            {
                
            }
            else if (sender == btnShowVideoLevelPopup)
            {
                
            }
        }
    }
}
