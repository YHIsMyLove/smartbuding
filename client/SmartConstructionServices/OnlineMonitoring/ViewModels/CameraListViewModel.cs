using System;
using System.Collections.Generic;
using System.ComponentModel;
using SmartConstructionServices.Account.Models;
using SmartConstructionServices.Common;
using SmartConstructionServices.OnlineMonitoring.Services;

namespace SmartConstructionServices.OnlineMonitoring.ViewModels
{
    public class CameraListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CameraListViewModel()
        {
            cameraService = new CameraService();
            cameras = new List<Camera>();
        }

        public List<Camera> Cameras
        {
            get { return cameras; }
            private set 
            {
                if (cameras != value)
                {
                    cameras = value;
                    DoPropertyChanged("Cameras");
                }
            }
        }

        public void FetchCameras()
        {
            if (!ServiceContext.Instance.IsLogin()) return;

            List<Camera> newPage = cameraService.GetCameraList(page, pageSize);
            if (newPage.Count == 0) return;

            List<Camera> cameraList = new List<Camera>(cameras);
            cameraList.AddRange(newPage);
            page += 1;
            Cameras = cameraList;
        }

        private void DoPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Camera> cameras;
        private CameraService cameraService;
        private int page = 1;
        private int pageSize = 10;
        private int total;
    }
}
