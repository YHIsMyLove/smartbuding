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
    public class EventDetailViewModel : ViewModelBase
    {
        public EventDetailViewModel()
        {
            eventService = new EventService();
            RefreshCommand = new Command(execute: async () => { await Refresh(); }, canExecute: () => { return IsRefreshCommandCanExecute(); });
            FetchMoreCommand = new Command(execute: async () => { await FetchMore(); }, canExecute: () => { return IsFetchMoreCommandCanExecute(); });
        }

        private bool IsFetchMoreCommandCanExecute()
        {
            return !IsBusy;
        }

        private Task FetchMore()
        {
            throw new NotImplementedException();
        }

        private async Task Refresh()
        {
            page = 1;
            await FetchMeetingMinutes();
        }

        private bool IsRefreshCommandCanExecute()
        {
            return !IsBusy;
        }

        private async Task FetchMeetingMinutes()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            var result = await eventService.FetchMeetingMinutes(meeting, page, pageSize);
            IsBusy = false;
            HasError = result.HasError;
            Error = Error;

            if (!result.HasError)
            {
                mettingMinutes = result.Model;
            }
        }

        #region Properties

        public Meeting Meeting
        {
            get { return meeting; }
            set
            {
                if (meeting == value) return;
                meeting = value;
                NotifyPropertyChanged(nameof(Meeting));
            }
        }

        #endregion

        #region Command

        public ICommand RefreshCommand { get; set; }

        public ICommand FetchMoreCommand { get; set; }

        #endregion

        #region Fields

        private EventService eventService;
        private Meeting meeting;
        private IList<MeetingMinutes> mettingMinutes;
        private int page = 1;
        private int pageSize = 10;

        #endregion
    }
}
