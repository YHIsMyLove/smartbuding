using SmartConstructionServices.Common;
using SmartConstructionServices.PeopleManagement.Models;
using SmartConstructionServices.PeopleManagement.Services;
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
        }

        #region Methods

        private bool IsFetchContactsCommandCanExecute()
        {
            return !isBusy;
        }

        private async Task FetchContacts()
        {
            if (isBusy) return;
            SetProperty(ref nomoreData, false, nameof(nomoreData));
            SetProperty(ref isBusy, true, nameof(IsBusy));
            page = 1;
            var result = await contactsService.Find(SelectedProject, SelectedDepartment, 1, pageSize);
            SetProperty(ref isBusy, false, nameof(IsBusy));
            if (result.HasError)
            {
                SetProperty(ref hasError, true, nameof(HasError));
                SetProperty(ref error, result.Error, nameof(Error));
            }
            else
            {
                List<Contacts> list = new List<Contacts>(result.Model);
                SetProperty(ref contacts, list, nameof(Contacts));
            }
        }

        private bool IsFetchMoreContactsCommandCanExecute()
        {
            return !isBusy;
        }

        private async Task FetchMoreContacts()
        {
            if (isBusy) return;
            SetProperty(ref isBusy, true, nameof(IsBusy));
            var result = await contactsService.Find(SelectedProject, SelectedDepartment, page, pageSize);
            SetProperty(ref isBusy, false, nameof(IsBusy));
            if (result.HasError)
            {
                SetProperty(ref hasError, true, nameof(HasError));
                SetProperty(ref error, result.Error, nameof(Error));
            }
            else
            {
                if (result.Model.Count == 0)
                {
                    SetProperty(ref nomoreData, true, nameof(NomoreData));
                }
                else
                {
                    page++;
                    List<Contacts> prevContacts = contacts;
                    prevContacts.AddRange(result.Model);
                    SetProperty(ref contacts, null, nameof(Contacts));
                    SetProperty(ref contacts, prevContacts, nameof(Contacts));
                }
            }
        }

        private void ClearError()
        {
            SetProperty(ref hasError, false, nameof(ClearError));
        }

        #endregion

        #region Properties
        public IList<Contacts> Contacts
        {
            get { return contacts; }
        }

        public IList<string> Projects
        {
            get { return projects; }
        }

        public IList<string> Departments
        {
            get { return departments; }
        }

        public string SelectedProject { get; set; }

        public string SelectedDepartment { get; set; }

        public bool NomoreData { get { return nomoreData; } }
        #endregion

        #region Commands

        public ICommand FetchContactsCommand { get; set; }

        public ICommand FetchMoreContactsCommand { get; set; }

        #endregion

        private ContactsService contactsService;
        private List<Contacts> contacts;
        private List<string> projects;
        private List<string> departments;
        private int page = 1;
        private int pageSize = 10;
        private bool nomoreData;
    }
}
