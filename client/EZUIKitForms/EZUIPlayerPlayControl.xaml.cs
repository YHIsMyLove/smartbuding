using System;
using System.Collections.Generic;
using EZOpenSDKForms;
using Xamarin.Forms;
using System.Windows.Input;

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

        public static readonly BindableProperty MaximizeCommandProperty =
            BindableProperty.Create(nameof(MaximizeCommand), typeof(ICommand), typeof(EZUIPlayerPlayControl), null,
                propertyChanged:(bindable, oldValue, newValue)=> {
                    EZUIPlayerPlayControl playControl = (EZUIPlayerPlayControl)bindable;
                    if (oldValue != null)
                    {
                        playControl.RemoveEventForMaximizeCommand((ICommand)oldValue);   
                    }
                    if (newValue != null)
                    {
                        playControl.AddEventForMaximizeCommand((ICommand)newValue);
                    }
                });

        public ICommand MaximizeCommand
        {
            get { return (ICommand)GetValue(MaximizeCommandProperty); }
            set { SetValue(MaximizeCommandProperty, value); }
        }

        public static readonly BindableProperty MaximizeCommandParameterProperty =
            BindableProperty.Create(nameof(MaximizeCommandParameter), typeof(object), typeof(EZUIPlayerPlayControl), null);

        public object MaximizeCommandParameter {
            get { return GetValue(MaximizeCommandParameterProperty); }
            set { SetValue(MaximizeCommandParameterProperty, value); }
        }

        private void AddEventForMaximizeCommand(ICommand newValue)
        {
            newValue.CanExecuteChanged += MaximizeCommand_CanExecuteChanged;
        }

        private void MaximizeCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            btnFullScreen.IsEnabled = MaximizeCommand.CanExecute(MaximizeCommandParameter);
        }

        private void RemoveEventForMaximizeCommand(ICommand oldValue)
        {
            oldValue.CanExecuteChanged -= MaximizeCommand_CanExecuteChanged;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (sender == btnMute)
            {
                
            }
            else if (sender == btnPlay)
            {
                if (Player == null) return;
                switch (Player.Status)
                {
                    case VideoStatus.NotReady:
                        break;
                    case VideoStatus.Playing:
                        Player.Stop();
                        break;
                    case VideoStatus.Stoped:
                        Player.Play();
                        break;
                }
            }
            else if (sender == btnChangeView)
            {
                //todo
            }
            else if (sender == btnFullScreen)
            {
                if (MaximizeCommand == null) return;
                MaximizeCommand.Execute(MaximizeCommandParameter);
            }
            else if (sender == btnShowVideoLevelPopup)
            {
                ShowPopup();
            }
        }

        private void ShowPopup()
        {
            Picker picker = new Picker
            {
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker.Items.Add("高清");
            picker.Items.Add("标清");
            picker.Items.Add("省流量");

            picker.SelectedIndexChanged += (sender, args) =>
            {
                switch (picker.SelectedIndex)
                {
                    case 0:
                        
                        break;
                    case 1:

                        break;

                    case 2:

                        break;
                }
                btnShowVideoLevelPopup.Text = (string)picker.SelectedItem;
            };
        }
    }
}
