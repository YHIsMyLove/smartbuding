using SmartConstructionServices.Account.ViewModels;
using SmartConstructionServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMainPage : ContentPage
    {
        private UserMainViewModel viewModel;

        public UserMainPage()
        {
            viewModel = new UserMainViewModel();
            BindingContext = viewModel;
            InitializeComponent();
            if (ServiceContext.Instance.CurrentUser != null)
            {
                imageUserPhoto.Source = new UriImageSource
                {
                    Uri = new Uri(ServiceContext.Instance.CurrentUser.UserHeadImg),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(5, 0, 0, 0)
                };
            }
        }

        public ListView ListView
        {
            get { return listView; }
        }
    }
}