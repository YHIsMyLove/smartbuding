using System;
using System.Collections;
using System.Collections.Generic;
using SmartConstructionServices.Account.Models;
using SmartConstructionServices.Common;

namespace SmartConstructionServices.OnlineMonitoring.Services
{
    public class CameraService
    {
        public CameraService()
        {
        }

        public List<Camera> GetCameraList(int page, int pageSize)
        {
            if (!ServiceContext.Instance.IsLogin()) return new List<Camera>();
            List<Camera> cameras = new List<Camera>();
            //test
            for (int i = 0; i < pageSize; i++)
            {
                Camera c;
                if (i % 2 == 0)
                {
                    c = new Camera()
                    {
                        Name = "Camera_" + (i + 1),
                        LiveStreamingUrl = "http://hls.open.ys7.com/openlive/2260bca2bf114d7a975bdee876c08bde.m3u8",
                        SnapshotPath = "test_snapshot.jpg"
                    };
                }
                else
                {
                    c = new Camera()
                    {
                        Name = "Camera_" + (i + 1),
                        LiveStreamingUrl = "http://hls.open.ys7.com/openlive/b310a6005c644be991510ace062cb1db.m3u8",
                        SnapshotPath = "test_snapshot.jpg"
                    };
                }
                cameras.Add(c);
            }
            //test
            return cameras;
        }
    }
}
