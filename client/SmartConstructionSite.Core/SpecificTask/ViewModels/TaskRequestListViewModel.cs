using System;
using System.Collections.ObjectModel;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.SpecificTask.Models;

namespace SmartConstructionSite.Core.SpecificTask.ViewModels
{
    public class TaskRequestListViewModel
    {
        public TaskRequestListViewModel()
        {
            TaskRequests = new ObservableCollection<TaskRequest>(SimpleData.Instance.GetTaskRequests());
        }

        public ObservableCollection<TaskRequest> TaskRequests
        {
            get;
            private set;
        }
    }
}
