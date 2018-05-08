using System;
namespace SmartConstructionSite.Core.Common
{
    public interface ISaveAndLoad
    {
        void SaveText(string filename, string text);
        string LoadText(string filename);
        string LoadAsset(string assetName);
    }
}
