using Plugin.Messaging;
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
            return !calling && !IsBusy;
        }

        private void MakePhoneCall()
        {
            if (calling) return;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(contacts.PhoneNumber);
        }

        #region Properties

        public Contacts Contacts {
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

        private Contacts contacts;
        private bool calling;

        #endregion
    }
}
