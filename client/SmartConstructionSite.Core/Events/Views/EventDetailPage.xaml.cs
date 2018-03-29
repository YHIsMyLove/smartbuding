using BottomBar.XamarinForms;
using SmartConstructionSite.Core.Events.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.Events.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : BottomBarPage
    {
        private EventDetailViewModel viewModel;

        public EventDetailPage()
        {
            viewModel = new EventDetailViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}