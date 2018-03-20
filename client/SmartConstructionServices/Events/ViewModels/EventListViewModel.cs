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
            years = SimpleData.Instance.GetYears();
            months = SimpleData.Instance.GetMonths();
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

        public List<string> Years
        {
            get { return years; }
            set
            {
                if (years == value) return;
                years = value;
                NotifyPropertyChanged(nameof(Years));
            }
        }

        public List<string> Months
        {
            get { return months; }
            set
            {
                if (months == value) return;
                months = value;
                NotifyPropertyChanged(nameof(Months));
            }
        }

        public List<string> Days
        {
            get { return days; }
            set
            {
                if (days == value) return;
                days = value;
                NotifyPropertyChanged(nameof(Days));
            }
        }

        public string SelectedYear
        {
            get { return selectedYear; }
            set
            {
                if (selectedYear == value) return;
                selectedYear = value;
                NotifyPropertyChanged(nameof(SelectedYear));
                UpdateDays();
            }
        }

        private void UpdateDays()
        {
            if (selectedYear == null || selectedMonth == null || "无".Equals(selectedYear) || "无".Equals(selectedMonth)) return;
            Days = SimpleData.Instance.GetDays(Convert.ToInt32(selectedYear), Convert.ToInt32(selectedMonth));
        }

        public string SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                if (selectedMonth == value) return;
                selectedMonth = value;
                NotifyPropertyChanged(nameof(SelectedMonth));
                UpdateDays();
            }
        }

        public string SelectedDay
        {
            get { return selectedDay; }
            set
            {
                if (selectedDay == value) return;
                selectedDay = value;
                NotifyPropertyChanged(nameof(SelectedDay));
            }
        }

        #endregion

        #region Commands

        public ICommand FetchLatestEventsCommand { get; set; }

        #endregion

        #region Fields

        private EventService eventService;
        private ObservableCollection<PageTypeGroup> meetings = new ObservableCollection<PageTypeGroup>();
        private List<string> years;
        private List<string> months;
        private List<string> days;
        private string selectedYear;
        private string selectedMonth;
        private string selectedDay;

        #endregion
    }
}
