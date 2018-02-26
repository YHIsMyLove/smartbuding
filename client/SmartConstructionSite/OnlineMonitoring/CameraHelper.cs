using System;
namespace SmartConstructionSite.OnlineMonitoring
{
    public class CameraHelper
    {
        public event EventHandler ShowCameraListRequested;

        public void ShowCameraList()
        {
            ShowCameraListRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
