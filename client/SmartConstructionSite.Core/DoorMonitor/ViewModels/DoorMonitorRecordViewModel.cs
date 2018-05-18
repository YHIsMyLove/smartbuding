using System;
using System.Collections.ObjectModel;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.DoorMonitor.Models;

namespace SmartConstructionSite.Core.DoorMonitor.ViewModels
{
	public class DoorMonitorRecordViewModel : ViewModelBase
    {
		private AttendanceStatistics statistics;

		public DoorMonitorRecordViewModel()
        {
			DoorStates = new ObservableCollection<DoorState>();
			DoorStates.Add(new DoorState() { DoorNumber = "#1", Who = "张三", Time = DateTime.Now, Action = "进场" });
			DoorStates.Add(new DoorState() { DoorNumber = "#2", Who = "李四", Time = DateTime.Now, Action = "出场" });
			DoorStates.Add(new DoorState() { DoorNumber = "#3", Who = "王五", Time = DateTime.Now, Action = "进场" });

			Statistics = new AttendanceStatistics()
			{
				Jc = 950,
				Cc = 453,
				Zc = 918,
				Xc = 341,
				Ycq = 950,
				Scq = 934,
				Cqzc = 930,
				Cd = 35,
				Zt = 16,
				Qq = 16
			};
        }

		public AttendanceStatistics Statistics
		{
			get { return statistics; }
			private set
			{
				if (statistics == value) return;
				statistics = value;
				NotifyPropertyChanged(nameof(Statistics));
			}
		}

		public ObservableCollection<DoorState> DoorStates
		{
			get;
			private set;
		}
    }
}
