using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EZUIKitForms
{
    public partial class EZUIPlapyerToolPanel : ContentView
    {
        public EZUIPlapyerToolPanel()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty PlayerProperty = BindableProperty.Create(nameof(Player), typeof(EZUIPlayer), null);

        public EZUIPlayer Player
        {
            get { return (EZUIPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        //public static readonly BindableProperty CurrentCameraProperty = BindableProperty.Create(nameof(CurrentCamera), typeof(Camera), null);

        //public Camera CurrentCamera
        //{
        //    get { return (Camera)GetValue(CurrentCameraProperty); }
        //    set { SetValue(CurrentCameraProperty, value); }
        //}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (btn == btnEye)
                RequestCoverCamera();
            else if (btn == btnMic)
                ShowDialoguePanel();
            else if (btn == btnCtrl)
                ShowCtrlPanel();
            else if (btn == btnCamera1)
                RequestCapture();
            else if (btn == btnCamera2)
                RequestRecord();
        }

        private void ShowCtrlPanel()
        {
            
        }

        private void RequestRecord()
        {
            
        }

        private void RequestCapture()
        {
            
        }

        private void ShowDialoguePanel()
        {
            
        }

        private void RequestCoverCamera()
        {
            
        }
    }
}
