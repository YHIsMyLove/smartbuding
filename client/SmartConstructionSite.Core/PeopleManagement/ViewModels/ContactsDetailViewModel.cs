using Plugin.Messaging;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.PeopleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.PeopleManagement.ViewModels
{
    public class ContactsDetailViewModel : ViewModelBase
    {
        public ContactsDetailViewModel()
        {
            PhoneCallCommand = new Command(execute: () => { MakePhoneCall(); }, canExecute: () => { return IsPhoneCallCommandCanExecute(); });
        }

        private bool IsPhoneCallCommandCanExecute()
        {
            return !IsBusy;
        }

        private void MakePhoneCall()
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(contacts.UserPhoneNum);
        }

        #region Properties

        public User Contacts {
            get { return contacts; }
            set {
                if (contacts == value) return;
                contacts = value;
                NotifyPropertyChanged(nameof(Contacts));
            }
        }

        public ICommand PhoneCallCommand { get; set; }

        #endregion

        #region Fields

        private User contacts;

        #endregion
    }
}
