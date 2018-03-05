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
            viewModel.FetchProvinces();
		}

        ProjectListViewModel viewModel;
	}
}