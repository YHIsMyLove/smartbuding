using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.ProjectManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.ProjectManagement.ViewModels
{
    public class ProjectListViewModel : ViewModelBase
    {
        public ProjectListViewModel()
        {
            projectService = new ProjectService();
            FindProjectsCommand = new Command(execute: async () => { await FindProjects(); }, canExecute: () => { return IsFindProjectCommandCanExecute(); });
            FetchProvincesCommand = new Command(execute: async () => { await FetchProvinces(); }, canExecute: () => { return IsFetchProvincesCommandCanExecute(); });
            FetchCitiesCommand = new Command(execute: async () => { await FetchCities(); }, canExecute: () => { return IsFetchCitiesCommandCanExecute(); });

            provinces = SimpleData.Instance.GetProvinces();
            selectedProvince = provinces[0];
            cities = SimpleData.Instance.GetCities(selectedProvince);
            selectedCity = cities[0];
            projects = SimpleData.Instance.GetProjects(selectedProvince, selectedCity);
        }

        #region Properties
        public string SelectedProvince
        {
            get => selectedProvince;
            set
            {
                if (selectedProvince == value) return;
                selectedProvince = value;
                NotifyPropertyChanged(nameof(SelectedProvince));

                //update cities and projects
                Cities = SimpleData.Instance.GetCities(selectedProvince);
                Projects = SimpleData.Instance.GetProjects(selectedProvince);
                selectedCity = Cities[0];
            }
        }

        public string SelectedCity {
            get => selectedCity;
            set {
                if (selectedCity == value) return;
                selectedCity = value;
                NotifyPropertyChanged(nameof(SelectedCity));

                //update projects
                Projects = SimpleData.Instance.GetProjects(selectedProvince, selectedCity);
            }
        }

        public IList<string> Projects {
            get => projects;
            private set {
                if (projects == value) return;
                projects = value;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public IList<string> Provinces {
            get => provinces;
            private set {
                if (provinces == value) return;
                provinces = value;
                NotifyPropertyChanged(nameof(Provinces));
            }
        }

        public IList<string> Cities {
            get => cities;
            private set {
                if (cities == value) return;
                cities = value;
                NotifyPropertyChanged(nameof(Cities));
            }
        }
        #endregion

        #region Command
        public ICommand FindProjectsCommand { private set; get; }

        public ICommand FetchProvincesCommand { private set; get; }

        public ICommand FetchCitiesCommand { private set; get; }

        #endregion

        private async Task FindProjects()
        {
            if (IsBusy) return;
            IsBusy = true;
            var result = await projectService.FindProjects(selectedProvince, selectedCity);
            Projects = result.Model;
            IsBusy = false;
        }

        private async Task FetchProvinces()
        {
            if (IsBusy) return;
            IsBusy = true;
            var result = await projectService.FetchProvinces();
            Provinces = result.Model;
            if (Provinces.Count() > 0)
            {
                result = await projectService.FetchCities(Provinces[0]);
                Cities = result.Model;
            }
            IsBusy = false;
        }

        private async Task FetchCities()
        {
            if (IsBusy) return;
            IsBusy = true;
            var result = await projectService.FetchCities(selectedProvince);
            Cities = result.Model;
            IsBusy = false;
        }

        private bool IsFindProjectCommandCanExecute()
        {
            return !IsBusy;
        }

        private bool IsFetchProvincesCommandCanExecute()
        {
            return !IsBusy;
        }

        private bool IsFetchCitiesCommandCanExecute()
        {
            return !IsBusy;
        }

        private ProjectService projectService;
        private string selectedProvince;
        private string selectedCity;
        private IList<string> projects;
        private IList<string> provinces;
        private IList<string> cities;
    }
}
