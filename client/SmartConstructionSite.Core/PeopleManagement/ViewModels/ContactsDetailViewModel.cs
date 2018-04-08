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
                //if (contacts != null)
                //{
                //    //contacts.Certificates = new List<string>()
                //    //{
                //    //    "icon.png", "ic_forklift.png", "ic_chimneys.png"
                //    //};
                //    //contacts.Certificates = new List<string>()
                //    //{
                //    //    "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=1672829216,536804227&fm=27&gp=0.jpg",
                //    //    "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=3288225748,2297060601&fm=27&gp=0.jpg",
                //    //    "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1522997762119&di=bf175aadf4be9979dfc0f7f04fd12d2b&imgtype=0&src=http%3A%2F%2Fpic1.win4000.com%2Fwallpaper%2F7%2F58b0f624125c1.jpg%3Fdown"
                //    //};
                //}
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
