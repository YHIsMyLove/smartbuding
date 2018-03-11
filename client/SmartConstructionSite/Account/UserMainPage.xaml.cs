using SmartConstructionServices.Account.ViewModels;
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
        }

        public ListView ListView
        {
            get { return listView; }
        }
    }
}