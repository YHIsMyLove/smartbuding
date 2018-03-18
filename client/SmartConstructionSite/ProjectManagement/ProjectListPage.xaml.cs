using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartConstructionServices.ProjectManagement.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.ProjectManagement
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectListPage : ContentPage
	{
		public ProjectListPage ()
		{
            viewModel = new ProjectListViewModel();
            BindingContext = viewModel;
			InitializeComponent ();
		}

        ProjectListViewModel viewModel;

        private void pickerProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("无".Equals(pickerProvinces.SelectedItem))
                pickerCities.IsEnabled = false;
            else
                pickerCities.IsEnabled = true;
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PopAsync(true);
            listView.SelectedItem = null;
        }
    }
}