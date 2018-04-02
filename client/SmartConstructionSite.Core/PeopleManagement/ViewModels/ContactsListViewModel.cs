using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.PeopleManagement.Models;
using SmartConstructionSite.Core.PeopleManagement.Services;
using SmartConstructionSite.Core.ProjectManagement.Models;
using SmartConstructionSite.Core.ProjectManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.PeopleManagement.ViewModels
{
    public class ContactsListViewModel : ViewModelBase
    {
        public ContactsListViewModel()
        {
            contactsService = new ContactsService();
            projectService = new ProjectService();
            FetchContactsCommand = new Command(execute: async () => { await FetchContacts(); }, canExecute: () => { return IsFetchContactsCommandCanExecute(); });
            FetchMoreContactsCommand = new Command(execute: async () => { await FetchMoreContacts(); }, canExecute: () => { return IsFetchMoreContactsCommandCanExecute(); });

            //projects = SimpleData.Instance.GetProjects(ServiceContext.Instance.Region);
            //contacts = SimpleData.Instance.GetContacts(ServiceContext.Instance.CurrentProject);
            InitData();
        }

        async Task InitData()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;

            var result = await projectService.FindProjects();
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            IsBusy = false;
            foreach (var item in result.Model)
            {
                Projects.Add(item);
            }
            if (Projects.Count != 0)
                SelectedProject = ServiceContext.Instance.CurrentProject;
        }

        #region Methods

        private bool IsFetchContactsCommandCanExecute()
        {
            return !IsBusy;
        }

        private async Task FetchContacts()
        {
            if (IsBusy) return;
            NomoreData = false;
            HasError = false;
            Error = null;
            IsBusy = true;
            page = 1;
            Contacts.Clear();
            var result = await contactsService.Find(SelectedProject, SelectedDepartment, 1, pageSize);
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            foreach (var item in result.Model)
            {
                Contacts.Add(item);
            }
            IsBusy = false;
        }

        private bool IsFetchMoreContactsCommandCanExecute()
        {
            return !IsBusy;
        }

        private async Task FetchMoreContacts()
        {
            if (IsBusy) return;
            IsBusy = true;
            IsLoadingMoreData = true;
            HasError = false;
            Error = null;
            var result = await contactsService.Find(SelectedProject, SelectedDepartment, page, pageSize);
            IsLoadingMoreData = false;
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            if (result.Model.Count == 0)
            {
                NomoreData = true;
            }
            else
            {
                page++;
                foreach (var item in result.Model)
                {
                    Contacts.Add(item);
                }
            }
        }

        #endregion

        #region Properties
        public ObservableCollection<User> Contacts {
            get { return contacts; }
            private set {
                if (contacts == value) return;
                contacts = value;
                NotifyPropertyChanged(nameof(Contacts));
            }
        }

        public ObservableCollection<Project> Projects {
            get { return projects; }
            private set {
                if (projects == value) return;
                projects = value;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public ObservableCollection<Department> Departments {
            get { return departments; }
            private set {
                if (departments == value) return;
                departments = value;
                NotifyPropertyChanged(nameof(Departments));
            }
        }

        public Project SelectedProject {
            get { return selectedProject; }
            set {
                if (selectedProject == value) return;
                selectedProject = value;
                NotifyPropertyChanged(nameof(SelectedProject));

                //Update departments and contacts
                //if ("无".Equals(selectedProject))
                //{
                //    SelectedDepartment = "无";
                //    Contacts = SimpleData.Instance.GetContacts(ServiceContext.Instance.CurrentProject);
                //}
                //else
                //{
                //    Departments = SimpleData.Instance.GetDepartments(selectedProject);
                //    Contacts = SimpleData.Instance.GetContacts(selectedProject);
                //}
                UpdateDeptAndContacts();
            }
        }

        private async Task UpdateDeptAndContacts()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            Departments.Clear();
            Contacts.Clear();
            var result = await contactsService.FetchDepartments(selectedProject);
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
            }
            if (result.Model.Count != 0)
            {
                result.Model.Insert(0, null);
            }
            foreach (var item in result.Model)
            {
                Departments.Add(item);
            }
            var result1 = await contactsService.Find(selectedProject);
            if (result1.HasError)
            {
                HasError = true;
                Error = result1.Error;
                IsBusy = false;
                return;
            }
            foreach (var item in result1.Model)
            {
                Contacts.Add(item);
            }
            IsBusy = false;
        }

        public Department SelectedDepartment {
            get { return selectedDepartment; }
            set {
                if (selectedDepartment == value) return;
                selectedDepartment = value;
                NotifyPropertyChanged(nameof(SelectedDepartment));

                //if (!"无".Equals(selectedDepartment))
                //    Contacts = SimpleData.Instance.GetContacts(selectedProject, selectedDepartment);
                //else
                //{
                //    if (!"无".Equals(selectedProject))
                //    {
                //        Contacts = SimpleData.Instance.GetContacts(selectedProject);
                //    }
                //}
                FetchContacts();
            }
        }

        public bool NomoreData {
            get { return nomoreData; }
            set {
                if (nomoreData == value) return;
                nomoreData = value;
                NotifyPropertyChanged(nameof(NomoreData));
            }
        }

        public bool IsLoadingMoreData {
            get { return isLoadingMoreData; }
            set {
                if (isLoadingMoreData == value) return;
                isLoadingMoreData = value;
                NotifyPropertyChanged(nameof(IsLoadingMoreData));
            }
        }

        #endregion

        #region Commands

        public ICommand FetchContactsCommand { get; set; }

        public ICommand FetchMoreContactsCommand { get; set; }

        #endregion

        private ContactsService contactsService;
        private ProjectService projectService;
        private ObservableCollection<User> contacts = new ObservableCollection<User>();
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        private ObservableCollection<Department> departments = new ObservableCollection<Department>();
        private Project selectedProject;
        private Department selectedDepartment;
        private int page = 1;
        private int pageSize = 10;
        private bool nomoreData;
        private bool isLoadingMoreData;
    }
}
