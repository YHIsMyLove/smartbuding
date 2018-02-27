using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionServices.OnlineMonitoring.ViewModels
{
    public class CameraLiveStreamingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CameraLiveStreamingViewModel()
        {
            
        }

        public Camera Camera
        {
            get { return camera; }
            set
            {
                if (camera != value)
                {
                    camera = value;
                    DoPropertyChanged("Camera");
                }
            }
        }

        private void DoPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Camera camera;
        private bool maximized;
    }
}
