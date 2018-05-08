using System;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace SmartConstructionSite.Droid
{
    public class BaseUrl_Android : IBaseUrl
    {
        public BaseUrl_Android()
        {
        }

        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}
