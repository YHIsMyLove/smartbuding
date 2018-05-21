using SmartConstructionServices.Common;
using SmartConstructionServices.PeopleManagement.Models;
using SmartConstructionServices.PeopleManagement.Services;
using SmartConstructionServices.ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionServices.PeopleManagement.ViewModels
{
    public class ContactsListViewModel : ViewModelBase
    {
        public ContactsListViewModel()
        {
            contactsService = new ContactsService();
            FetchContactsCommand = new Command(execute: async () => { await FetchContacts(); }, canExecute: () => { return IsFetchContactsCommandCanExecute(); });
            FetchMoreContactsCommand = new Command(execute: async () => { await FetchMoreContacts(); }, canExecute: () => { return IsFetchMoreContactsCommandCanExecute(); });

            projects = SimpleData.Instance.GetProjects(ServiceContext.Instance.Region);
            contacts = SimpleData.Instance.GetContacts(ServiceContext.Instance.CurrentProject);
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
            var result = await contactsService.Find(SelectedProject, SelectedDepartment, 1, pageSize);
            IsBusy = false;
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
            }
            else
            {
                List<Contacts> list = new List<Contacts>(result.Model);
                Contacts = list;
            }
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
            IsBusy = false;
            IsLoadingMoreData = false;
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
            }
            else
            {
                if (result.Model.Count == 0)
                {
                    NomoreData = true;
                }
                else
                {
                    page++;
                    List<Contacts> newContacts = new List<Contacts>(contacts);
                    newContacts.AddRange(result.Model);
                    Contacts = newContacts;
                }
            }
        }

        #endregion

        #region Properties
        public IList<Contacts> Contacts {
            get { return contacts; }
            private set {
                if (contacts == value) return;
                contacts = value;
                NotifyPropertyChanged(nameof(Contacts));
            }
        }

        public IList<string> Projects {
            get { return projects; }
            private set {
                if (projects == value) return;
                projects = value;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public IList<string> Departments {
            get { return departments; }
            private set {
                if (departments == value) return;
                departments = value;
                NotifyPropertyChanged(nameof(Departments));
            }
        }

        public string SelectedProject {
            get { return selectedProject; }
            set {
                if (selectedProject == value) return;
                selectedProject = value;
                NotifyPropertyChanged(nameof(SelectedProject));

                //Update departments and contacts
                if ("无".Equals(selectedProject))
                {
                    SelectedDepartment = "无";
                    Contacts = SimpleData.Instance.GetContacts(ServiceContext.Instance.CurrentProject);
                }
                else
                {
                    Departments = SimpleData.Instance.GetDepartments(selectedProject);
                    Contacts = SimpleData.Instance.GetContacts(selectedProject);
                }
            }
        }

        public string SelectedDepartment {
            get { return selectedDepartment; }
            set {
                if (selectedDepartment == value) return;
                selectedDepartment = value;
                NotifyPropertyChanged(nameof(SelectedDepartment));

                if (!"无".Equals(selectedDepartment))
                    Contacts = SimpleData.Instance.GetContacts(selectedProject, selectedDepartment);
                else
                {
                    if (!"无".Equals(selectedProject))
                    {
                        Contacts = SimpleData.Instance.GetContacts(selectedProject);
                    }
                }
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
        private IList<Contacts> contacts;
        private IList<string> projects;
        private IList<string> departments;
        private string selectedProject;
        private string selectedDepartment;
        private int page = 1;
        private int pageSize = 10;
        private bool nomoreData;
        private bool isLoadingMoreData;
    }
}
