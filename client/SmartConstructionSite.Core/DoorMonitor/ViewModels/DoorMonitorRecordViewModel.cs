using System;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.DoorMonitor.Models;

namespace SmartConstructionSite.Core.DoorMonitor.ViewModels
{
	public class DoorMonitorRecordViewModel : ViewModelBase
    {
		private AttendanceStatistics statistics;

		public DoorMonitorRecordViewModel()
        {
			Statistics = new AttendanceStatistics()
			{
				Total = 950,
				Real = 934,
				Normal = 930,
				Tardy = 35,
				LeaveEarly = 16,
				Absenteeism = 16
			};
        }

		public AttendanceStatistics Statistics
		{
			get { return statistics; }
			set
			{
				if (statistics == value) return;
				statistics = value;
				NotifyPropertyChanged(nameof(Statistics));
			}
		}
    }
}
