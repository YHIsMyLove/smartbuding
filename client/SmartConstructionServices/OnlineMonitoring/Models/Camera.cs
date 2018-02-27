using System;
using SmartConstructionServices.Common;

namespace SmartConstructionServices.OnlineMonitoring
{
    public class Camera : ModelBase
    {
        public Camera()
        {
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string SnapshotPath
        {
            get { return snapshotPath; }
            set { snapshotPath = value; }
        }

        public bool IsSupportControl
        {
            get { return isSupportControl; }
            set { isSupportControl = value; }
        }

        public string LiveStreamingUrl
        {
            get { return liveStreamingUrl; }
            set { liveStreamingUrl = value; }
        }

        public override string ToString()
        {
            return string.Format("[Camera: Name={0}, SnapshotPath={1}, IsSupportControl={2}, LiveStreamingUrl={3}]", Name, SnapshotPath, IsSupportControl, LiveStreamingUrl);
        }

        private string name;
        private string snapshotPath;
        private bool isSupportControl;
        private string liveStreamingUrl;
    }
}
