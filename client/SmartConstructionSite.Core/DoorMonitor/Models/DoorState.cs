using System;
using System.ComponentModel;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;

namespace SmartConstructionSite.Core.DoorMonitor.Models
{
	public class DoorState : ModelBase, INotifyPropertyChanged
    {      
		public event PropertyChangedEventHandler PropertyChanged;

        public DoorState()
        {
        }

        /// <summary>
        /// 获取或设置门号
        /// </summary>
        /// <value>The door number.</value>
		public string DoorNumber
		{
			get;
			set;
		}

        /// <summary>
        /// 获取或设置人名
        /// </summary>
        /// <value>The who.</value>
		public string Who
		{
			get { return who; }
			set
			{
				if (who == value) return;
				who = value;
				NotifyPropertyChanged(nameof(who));
			}
		}
        
        /// <summary>
        /// 获取或设置时间
        /// </summary>
        /// <value>The time.</value>
		public DateTime Time
		{
			get { return time; }
			set
			{
				if (time == value) return;
				time = value;
				NotifyPropertyChanged(nameof(time));
			}
		}

        /// <summary>
        /// 获取或设置动作，比如：进场或出场
        /// </summary>
        /// <value>The direction.</value>
		public string Action
		{
			get { return action; }
			set
			{
				if (action == value) return;
				action = value;
				NotifyPropertyChanged(nameof(action));
			}
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        private string who;
        private string action;
        private DateTime time;
	}
}
