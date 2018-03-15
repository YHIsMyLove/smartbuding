using SmartConstructionServices.Common;
using SmartConstructionServices.Events.Models;
using SmartConstructionServices.Events.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionServices.Events.ViewModels
{
    public class EventListViewModel : ViewModelBase
    {
        public EventListViewModel()
        {
            eventService = new EventService();
            FetchLatestEventsCommand = new Command(execute: async () => { await FetchLatestEvents(); }, canExecute: () => { return IsFetchLatestEventsCommandCanExecute(); });
        }

        private bool IsFetchLatestEventsCommandCanExecute()
        {
            return !IsBusy;
        }

        private async Task FetchLatestEvents()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            var result = await eventService.FetchLatestEvent();
            IsBusy = false;
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
            }
            else
            {
                List<Meeting> list = null;
                if (meetings == null)
                    list = new List<Meeting>();
                else
                    list = new List<Meeting>(meetings);
                for (int i = result.Model.Count - 1; i >= 0; i--)
                {
                    list.Insert(0, result.Model[i]);
                }
                Meetings = list;
            }
        }

        #region Properties

        public IList<Meeting> Meetings {
            get { return meetings; }
            set {
                if (meetings == value) return;
                meetings = value;
                NotifyPropertyChanged(nameof(Meetings));
            }
        }

        #endregion

        #region Commands

        public ICommand FetchLatestEventsCommand { get; set; }

        #endregion

        #region Fields

        private EventService eventService;
        private IList<Meeting> meetings;

        #endregion
    }
}
