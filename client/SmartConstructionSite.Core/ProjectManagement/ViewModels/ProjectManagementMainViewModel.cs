using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Events.Models;
using SmartConstructionSite.Core.Events.Services;
using SmartConstructionSite.Core.ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.ProjectManagement.ViewModels
{
    public class ProjectManagementMainViewModel : ViewModelBase
    {
        public ProjectManagementMainViewModel()
        {
            InitCommand = new Command(execute: async () => { await InitData(); });
            ChangeProjectCommand = new Command(execute: () => { ChangeProject(); }, canExecute: () => { return IsChangeProjectCommandCanExecute(); });


            project = ServiceContext.Instance.CurrentProject;
            LatestMeetings = new ObservableCollection<Meeting>();
        }

        public async Task UpdateMessageAndRelationalCount()
        {
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            LatestMeetings.Clear();
            var result = await eventService.FetchLatestEvent();
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            var latestMeetings = result.Model.Where((meeting) =>
            {
                var span = DateTime.Now - meeting.MeetingCreatedAt;
                return span.Days <= 5;
            });
            foreach (var item in latestMeetings)
            {
                LatestMeetings.Add(item);
            }
            var result1 = await eventService.GetRelationalCount();
            if (result1.HasError)
            {
                HasError = true;
                Error = result1.Error;
                IsBusy = false;
                return;
            }
            RelationalCount = result1.Model;
            IsBusy = false;
        }

        private bool IsChangeProjectCommandCanExecute()
        {
            return !IsBusy;
        }

        private void ChangeProject()
        {
            if (ServiceContext.Instance.CurrentUser == null || ServiceContext.Instance.CurrentProject == null) return;
            if (!ServiceContext.Instance.CurrentProject.Equals(project))
                Project = ServiceContext.Instance.CurrentProject;
        }

        private async Task InitData()
        {
            if (ServiceContext.Instance.CurrentUser == null || ServiceContext.Instance.CurrentProject == null) return;
            if (IsBusy || initilized) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            var result = await eventService.FetchLatestEvent();
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            var latestMeetings = result.Model.Where((meeting) =>
            {
                var span = DateTime.Now - meeting.MeetingCreatedAt;
                return span.Days <= 5;
            });
            foreach (var item in latestMeetings)
            {
                LatestMeetings.Add(item);
            }
            var result1 = await eventService.GetRelationalCount();
            if (result1.HasError)
            {
                HasError = true;
                Error = result1.Error;
                IsBusy = false;
                return;
            }
            RelationalCount = result1.Model;
            initilized = true;
            IsBusy = false;
        }

        public async Task UpdateRelationalCount()
        {
            if (ServiceContext.Instance.CurrentUser == null || ServiceContext.Instance.CurrentProject == null) return;
            if (IsBusy) return;
            IsBusy = true;
            HasError = false;
            Error = null;
            var result = await eventService.GetRelationalCount();
            if (result.HasError)
            {
                HasError = true;
                Error = result.Error;
                IsBusy = false;
                return;
            }
            RelationalCount = result.Model;
            IsBusy = false;
        }

        #region Properties

        public Project Project
        {
            get { return project; }
            private set
            {
                if (project == value) return;
                project = value;
                NotifyPropertyChanged(nameof(Project));
                ProjectInfo = project.Name;
                ProjectInfo = string.Format("{0}{1} | {2}", project.Prov.Name, project.City.Name, project.Name);
            }
        }

        public string ProjectInfo
        {
            get { return projectInfo; }
            private set
            {
                if (projectInfo == value) return;
                projectInfo = value;
                NotifyPropertyChanged(nameof(ProjectInfo));
            }
        }

        public int RelationalCount
        {
            get { return relationalCount; }
            set
            {
                if (relationalCount == value) return;
                relationalCount = value;
                NotifyPropertyChanged(nameof(RelationalCount));
            }
        }

        public ObservableCollection<Meeting> LatestMeetings { get; private set; }

        #endregion

        #region Commands

        public ICommand ChangeProjectCommand { get; private set; }

        public ICommand InitCommand { get; set; }

        #endregion

        #region Methods



        #endregion

        #region Fields

        private Project project;
        private string projectInfo;
        private EventService eventService = new EventService();
        private bool initilized;
        private int relationalCount;

        #endregion
    }
}
