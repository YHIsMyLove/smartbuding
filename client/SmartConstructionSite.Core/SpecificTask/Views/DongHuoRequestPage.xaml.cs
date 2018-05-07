using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.SpecificTask.Views
{
    public partial class DongHuoRequestPage : ContentPage
    {
        public DongHuoRequestPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}
