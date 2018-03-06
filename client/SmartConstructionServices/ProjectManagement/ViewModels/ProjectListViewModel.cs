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

namespace SmartConstructionServices.ProjectManagement.ViewModels
{
    public class ProjectListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectListViewModel()
        {
            projectService = new ProjectService();
            FindProjectsCommand = new Command(execute: () => { FindProjects(); }, canExecute: () => { return IsFindProjectCommandCanExecute(); });
            FetchProvincesCommand = new Command(execute: () => { FetchProvinces(); }, canExecute: () => { return IsFetchProvincesCommandCanExecute(); });
            FetchCitiesCommand = new Command(execute: () => { FetchCities(); }, canExecute: () => { return IsFetchCitiesCommandCanExecute(); });
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
            }
        }

        public string SelectedCity {
            get => selectedCity;
            set {
                if (selectedCity == value) return;
                selectedCity = value;
                DoPropertyChanged("SelectedCity");
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

        public IList<Project> Projects {
            get => projects;
            private set {
                if (projects == value) return;
                projects = (List<Project>)value;
                DoPropertyChanged("Projects");
            }
        }

        public IList<string> Provinces {
            get => provinces;
            private set {
                if (provinces == value) return;
                provinces = (List<string>)value;
                DoPropertyChanged("Provinces");
            }
        }

        public IList<string> Cities {
            get => cities;
            private set {
                if (cities == value) return;
                cities = (List<string>)value;
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
            Projects = await projectService.FindProjects(selectedProvince, selectedCity);
            DataLoading = false;
        }

        private async Task FetchProvinces()
        {
            if (dataLoading) return;
            DataLoading = true;
            Provinces = await projectService.FetchProvinces();
            if (Provinces.Count() > 0)
            {
                Cities = await projectService.FetchCities(Provinces[0]);
            }
            DataLoading = false;
        }

        private async Task FetchCities()
        {
            if (dataLoading) return;
            DataLoading = true;
            Cities = await projectService.FetchCities(selectedProvince);
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
        private List<Project> projects;
        private List<string> provinces;
        private List<string> cities;
    }
}
