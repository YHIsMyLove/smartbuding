using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.DoorMonitor.Models;
using SmartConstructionSite.Core.PeopleManagement.Models;

namespace SmartConstructionSite.Core.DoorMonitor.ViewModels
{
	public class StatisticsListViewModel : ViewModelBase
    {      
		public StatisticsListViewModel()
        {
			queryDate = new DateTime();
			departments = new List<Department>(){
				new Department(){ Name = "项目部" },
				new Department(){ Name = "工程部" }
			};
			groups = new List<string>() { 
			    "打桩队", "搅拌队"
			};
			queryDepartment = departments[0];
			queryGroup = groups[0];
			totalTime = 1023;
			items = new ObservableCollection<StatisticsItem>();
			items.Add(new StatisticsItem() {
				User = new User() { UserName = "ZhangSan", UserHeadImg = "user.png", WorkNumber = "001" },
				Time = 202,
				TimeIn = new DateTime(2018, 5, 14, 7, 30, 00),
				TimeOut = new DateTime(2018, 5, 14, 18, 30, 00),
				Unit = "四川羽灵建筑劳务有限公司"
			});
			items.Add(new StatisticsItem()
            {
				User = new User() { UserName = "LiSi", UserHeadImg = "user.png", WorkNumber = "002" },
                Time = 202,
                TimeIn = new DateTime(2018, 5, 14, 7, 30, 00),
                TimeOut = new DateTime(2018, 5, 14, 18, 30, 00),
                Unit = "四川羽灵建筑劳务有限公司"
            });
        }

		public DateTime QueryDate
		{
			get { return queryDate; }
			set
			{
				if (queryDate == value) return;
				queryDate = value;
				NotifyPropertyChanged(nameof(QueryDate));
			}
		}

		/// <summary>
        /// 获取或设置部门
        /// </summary>
        /// <value>The department.</value>
		public Department QueryDepartment
		{
			get { return queryDepartment; }
			set
			{
				if (queryDepartment == value) return;
				queryDepartment = value;
				NotifyPropertyChanged(nameof(QueryDepartment));
			}
		}
        
        /// <summary>
        /// 获取或设置班组.
        /// </summary>
        /// <value>The group.</value>
		public string QueryGroup
		{
			get { return queryGroup; }
			set
			{
				if (queryGroup == value) return;
				queryGroup = value;
				NotifyPropertyChanged(nameof(QueryGroup));
			}
		}

        /// <summary>
        /// 获取总时长
        /// </summary>
        /// <value>The total time.</value>
		public int TotalTime
		{
			get { return totalTime; }
			private set
			{
				if (totalTime == value) return;
				totalTime = value;
				NotifyPropertyChanged(nameof(TotalTime));
			}
		}

        public int TotalPepole
		{
			get { return totalPepole; }
			private set
			{
				if (totalPepole == value) return;
				totalPepole = value;
				NotifyPropertyChanged(nameof(TotalPepole));
			}
		}

		/// <summary>
        /// 获取部门列表
        /// </summary>
        /// <value>The departments.</value>
		public IList<Department> Departments
		{
			get { return departments; }
			private set
			{
				if (departments == value) return;
				departments = value;
				NotifyPropertyChanged(nameof(Departments));
			}
		}

        /// <summary>
        /// 获取班组列表
        /// </summary>
        /// <value>The groups.</value>
		public IList<string> Groups
		{
			get { return groups; }
			private set
			{
				if (groups == value) return;
				groups = value;
				NotifyPropertyChanged(nameof(Groups));
			}
		}

        /// <summary>
        /// 获取统计列表
        /// </summary>
        /// <value>The items.</value>
		public ObservableCollection<StatisticsItem> Items
		{
			get { return items; }
			private set
			{
				if (items == value) return;
				items = value;
				NotifyPropertyChanged(nameof(Items));
			}
		}

		private int totalTime;
		private string queryGroup;
		private Department queryDepartment;
		private IList<Department> departments;
		private IList<string> groups;
		private ObservableCollection<StatisticsItem> items;
		private DateTime queryDate;
		private int totalPepole;
	}
}
