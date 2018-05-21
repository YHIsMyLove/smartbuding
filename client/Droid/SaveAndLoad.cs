using System;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace SmartConstructionSite.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public SaveAndLoad()
        {
        }

        public string LoadAsset(string assetName)
        {
            var assetMgr = MainActivity.instance.Assets;
            using (var streamReader = new System.IO.StreamReader(assetMgr.Open(assetName)))
            {
                var html = streamReader.ReadToEnd();
                return html;
            }
        }

        public string LoadText(string filename)
        {
            throw new NotImplementedException();
        }

        public void SaveText(string filename, string text)
        {
            throw new NotImplementedException();
        }
    }
}
