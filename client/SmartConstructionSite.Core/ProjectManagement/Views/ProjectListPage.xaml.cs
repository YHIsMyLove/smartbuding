using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.ProjectManagement.Models;
using SmartConstructionSite.Core.ProjectManagement.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.ProjectManagement.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectListPage : ContentPage
    {
        ProjectListViewModel viewModel;

        public ProjectListPage ()
		{
            viewModel = new ProjectListViewModel();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            BindingContext = viewModel;
			InitializeComponent ();
		}

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.IsBusy))
            {
                if (viewModel.IsBusy)
                    UserDialogs.Instance.ShowLoading("正在登陆。。。", MaskType.Black);
                else
                    UserDialogs.Instance.HideLoading();
            }
            else if (e.PropertyName == nameof(viewModel.Error) && viewModel.Error != null)
            {
                var toastConfig = new ToastConfig(viewModel.Error.Description);
                toastConfig.SetDuration(3000);
                UserDialogs.Instance.Toast(toastConfig);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (ServiceContext.Instance.CurrentProject == null)
            {
                UserDialogs.Instance.Toast("请选择一个项目");
                return true;
            }
            else
                return base.OnBackButtonPressed();
        }

        private void pickerProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ("无".Equals(pickerProvinces.SelectedItem))
            //    pickerCities.IsEnabled = false;
            //else
                //pickerCities.IsEnabled = true;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
            //await Navigation.PopAsync(true);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ServiceContext.Instance.CurrentProject = e.Item as Project;
            await Navigation.PopAsync();
        }
    }
}