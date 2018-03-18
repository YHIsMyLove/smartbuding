using SmartConstructionServices.ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SmartConstructionServices.ProjectManagement.Services;
using SmartConstructionServices.Common;

namespace SmartConstructionServices.ProjectManagement.ViewModels
{
    public class ProjectListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                DoPropertyChanged("SelectedProvince");

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
                DoPropertyChanged("SelectedCity");

                //update projects
                Projects = SimpleData.Instance.GetProjects(selectedProvince, selectedCity);
            }
        }

        public bool DataLoading {
            get => dataLoading;
            private set {
                if (dataLoading == value) return;
                dataLoading = value;
                DoPropertyChanged("DataLoading");
            }
        }

        public IList<string> Projects {
            get => projects;
            private set {
                if (projects == value) return;
                projects = value;
                DoPropertyChanged("Projects");
            }
        }

        public IList<string> Provinces {
            get => provinces;
            private set {
                if (provinces == value) return;
                provinces = value;
                DoPropertyChanged("Provinces");
            }
        }

        public IList<string> Cities {
            get => cities;
            private set {
                if (cities == value) return;
                cities = value;
                DoPropertyChanged("Cities");
            }
        }
        #endregion

        #region Command
        public ICommand FindProjectsCommand { private set; get; }

        public ICommand FetchProvincesCommand { private set; get; }

        public ICommand FetchCitiesCommand { private set; get; }

        #endregion

        private void DoPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task FindProjects()
        {
            if (dataLoading) return;
            DataLoading = true;
            var result = await projectService.FindProjects(selectedProvince, selectedCity);
            Projects = result.Model;
            DataLoading = false;
        }

        private async Task FetchProvinces()
        {
            if (dataLoading) return;
            DataLoading = true;
            var result = await projectService.FetchProvinces();
            Provinces = result.Model;
            if (Provinces.Count() > 0)
            {
                result = await projectService.FetchCities(Provinces[0]);
                Cities = result.Model;
            }
            DataLoading = false;
        }

        private async Task FetchCities()
        {
            if (dataLoading) return;
            DataLoading = true;
            var result = await projectService.FetchCities(selectedProvince);
            Cities = result.Model;
            DataLoading = false;
        }

        private bool IsFindProjectCommandCanExecute()
        {
            return !dataLoading;
        }

        private bool IsFetchProvincesCommandCanExecute()
        {
            return !dataLoading;
        }

        private bool IsFetchCitiesCommandCanExecute()
        {
            return !dataLoading;
        }

        private ProjectService projectService;
        private string selectedProvince;
        private string selectedCity;
        private bool dataLoading;
        private IList<string> projects;
        private IList<string> provinces;
        private IList<string> cities;
    }
}
