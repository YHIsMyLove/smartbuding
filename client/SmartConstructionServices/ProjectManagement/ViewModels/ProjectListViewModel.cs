using SmartConstructionServices.ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionServices.ProjectManagement.ViewModels
{
    public class ProjectListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectListViewModel()
        {
            FindProjectCommand = new Command(execute: () => { FindProjects(); }, canExecute: () => { return RefreshFindProjectCommandCanExecute(); });
        }

        public string Province
        {
            get => province;
            set
            {
                if (province == value) return;
                province = value;
                DoPropertyChanged("Province");
            }
        }

        public string City {
            get => city;
            set {
                if (city == value) return;
                city = value;
                DoPropertyChanged("City");
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

        #region Command
        public ICommand FindProjectCommand { private set; get; }

        #endregion

        private void DoPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool RefreshFindProjectCommandCanExecute()
        {
            throw new NotImplementedException();
        }

        private void FindProjects()
        {
            throw new NotImplementedException();
        }

        private string province;
        private string city;
        private bool dataLoading;
        private List<Project> projects;
        private List<string> provinces;
        private List<string> cities;
        private ICommand Command;
    }
}
