using SmartConstructionServices.Common;
using SmartConstructionServices.Events.Models;
using SmartConstructionServices.Events.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            FetchLatestEvents();
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
                foreach (var item in result.Model)
                {
                    meetings.Add(item);
                }
            }
        }

        #region Properties

        public ObservableCollection<PageTypeGroup> Meetings {
            get { return meetings; }
            private set {
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
        private ObservableCollection<PageTypeGroup> meetings = new ObservableCollection<PageTypeGroup>();

        #endregion
    }
}
