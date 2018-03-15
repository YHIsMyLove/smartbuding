using SmartConstructionServices.Common;
using SmartConstructionServices.PeopleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.PeopleManagement.ViewModels
{
    public class ContactsDetailViewModel : ViewModelBase
    {

        #region Properties

        public Contacts Contacts {
            get { return contacts; }
            set {
                if (contacts == value) return;
                contacts = value;
                NotifyPropertyChanged(nameof(Contacts));
            }
        }

        #endregion

        #region Fields

        private Contacts contacts;

        #endregion
    }
}
