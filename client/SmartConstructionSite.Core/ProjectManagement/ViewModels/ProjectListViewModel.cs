﻿using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.ProjectManagement.Models;
using SmartConstructionSite.Core.ProjectManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //FetchProvincesCommand = new Command(execute: async () => { await FetchProvinces(); }, canExecute: () => { return IsFetchProvincesCommandCanExecute(); });
            FetchCitiesCommand = new Command(execute: async () => { await FetchCities(); }, canExecute: () => { return IsFetchCitiesCommandCanExecute(); });

            //provinces = SimpleData.Instance.GetProvinces();
            //selectedProvince = provinces[0];
            //cities = SimpleData.Instance.GetCities(selectedProvince);
            //selectedCity = cities[0];
            //projects = SimpleData.Instance.GetProjects(selectedProvince, selectedCity);
            InitData();
        }

        async Task InitData()
        {
            if (ServiceContext.Instance.CurrentUser == null) return;
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            var result = await projectService.FetchProvinces();
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            if (result.Model.Count != 0)
            {
                result.Model.Insert(0, null);
            }
            foreach (var item in result.Model)
            {
                Provinces.Add(item);
            }
            var result1 = await projectService.FindProjects();
            if (result1.HasError)
            {
                HasError = true;
                Error = result1.Error;
                IsBusy = false;
                return;
            }
            foreach (var item in result1.Model)
            {
                Projects.Add(item);
            }
            IsBusy = false;
        }

        #region Properties
        public Province SelectedProvince
        {
            get => selectedProvince;
            set
            {
                if (selectedProvince == value) return;
                selectedProvince = value;
                NotifyPropertyChanged(nameof(SelectedProvince));

                //update cities and projects
                //Cities = SimpleData.Instance.GetCities(selectedProvince);
                //Projects = SimpleData.Instance.GetProjects(selectedProvince);
                //selectedCity = Cities[0];
                UpdateCityAndProject();
            }
        }

        async Task UpdateCityAndProject()
        {
            if (IsBusy) return;
            Cities.Clear();
            Projects.Clear();
            IsBusy = true;
            HasError = false;
            Error = null;
            if (selectedProvince != null)
            {
                var result = await projectService.FetchCities(selectedProvince);
                if (result.HasError)
                {
                    HasError = true;
                    Error = result.Error;
                    IsBusy = false;
                    return;
                }
                foreach (var item in result.Model)
                {
                    Cities.Add(item);
                }
                if (Cities.Count != 0)
                    Cities.Insert(0, null);
            }
            var result1 = await projectService.FindProjects(selectedProvince, selectedCity);
            if (result1.HasError)
            {
                HasError = true;
                Error = result1.Error;
                IsBusy = false;
                return;
            }
            foreach (var item in result1.Model)
            {
                Projects.Add(item);
            }
            IsBusy = false;
        }

        public City SelectedCity {
            get => selectedCity;
            set {
                if (selectedCity == value) return;
                selectedCity = value;
                NotifyPropertyChanged(nameof(SelectedCity));

                //update projects
                //Projects = SimpleData.Instance.GetProjects(selectedProvince, selectedCity);
                FindProjects();
            }
        }

        public ObservableCollection<Project> Projects {
            get => projects;
            private set {
                if (projects == value) return;
                projects = value;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public ObservableCollection<Province> Provinces {
            get => provinces;
            private set {
                if (provinces == value) return;
                provinces = value;
                NotifyPropertyChanged(nameof(Provinces));
            }
        }

        public ObservableCollection<City> Cities {
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
            HasError = false;
            Error = null;
            Projects.Clear();
            var result = await projectService.FindProjects(selectedProvince, selectedCity);
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            foreach (var item in result.Model)
            {
                Projects.Add(item);
            }
            IsBusy = false;
        }

        private async Task FetchCities()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            Cities.Clear();
            var result = await projectService.FetchCities(selectedProvince);
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            foreach (var item in result.Model)
            {
                Cities.Add(item);
            }
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
        private Province selectedProvince;
        private City selectedCity;
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        private ObservableCollection<Province> provinces = new ObservableCollection<Province>();
        private ObservableCollection<City> cities = new ObservableCollection<City>();
    }
}
