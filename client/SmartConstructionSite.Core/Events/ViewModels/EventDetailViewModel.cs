using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Events.Models;
using SmartConstructionSite.Core.Events.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.Events.ViewModels
{
    public class EventDetailViewModel : ViewModelBase
    {
        public EventDetailViewModel(Meeting meeting)
        {
            this.meeting = meeting;
            eventService = new EventService();
            RefreshCommand = new Command(execute: async () => { await Refresh(); }, canExecute: () => { return IsRefreshCommandCanExecute(); });
            FetchMoreCommand = new Command(execute: async () => { await FetchMore(); }, canExecute: () => { return IsFetchMoreCommandCanExecute(); });
            ToggleFavoriteCommand = new Command(execute: (meetingMinutes) => { ToggleFavorite((MeetingMinutes)meetingMinutes); }, canExecute: (meetingMinute) => { return IsToggleFavoriteCommandCanExecute(); });
        }

        private void ToggleFavorite(MeetingMinutes meetingMinutes)
        {
            meetingMinutes.IsFavorite = !meetingMinutes.IsFavorite;
            int index = MeetingMinutes.IndexOf(meetingMinutes);
            MeetingMinutes.RemoveAt(index);
            MeetingMinutes.Insert(index, meetingMinutes);
        }

        private bool IsToggleFavoriteCommandCanExecute()
        {
            return !IsBusy;
        }

        private bool IsFetchMoreCommandCanExecute()
        {
            return !IsBusy;
        }

        private async Task FetchMore()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            page++;
            var result = await eventService.FetchMeetingMinutes(meeting, page, pageSize);
            IsBusy = false;
            HasError = result.HasError;
            Error = result.Error;
            if (!result.HasError && result.Model.Count > 0)
            {
                foreach (var item in result.Model)
                {
                    meetingMinutes.Add(item);
                }
            }
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
            meetingMinutes.Clear();
            var result = await eventService.FetchMeetingMinutes(meeting, page, pageSize);
            IsBusy = false;
            HasError = result.HasError;
            Error = Error;

            if (!result.HasError)
            {
                foreach (var item in result.Model)
                {
                    meetingMinutes.Add(item);
                }
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

        public ObservableCollection<MeetingMinutes> MeetingMinutes
        {
            get { return meetingMinutes; }
            set
            {
                if (meetingMinutes == value) return;
                meetingMinutes = value;
                NotifyPropertyChanged(nameof(MeetingMinutes));
            }
        }

        #endregion

        #region Command

        public ICommand RefreshCommand { get; set; }

        public ICommand FetchMoreCommand { get; set; }

        public ICommand ToggleFavoriteCommand { get; set; }

        #endregion

        #region Fields

        private EventService eventService;
        private Meeting meeting;
        private ObservableCollection<MeetingMinutes> meetingMinutes = new ObservableCollection<MeetingMinutes>();
        private int page = 1;
        private int pageSize = 10;

        #endregion
    }
}
