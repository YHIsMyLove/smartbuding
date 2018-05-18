using System;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;

namespace SmartConstructionSite.Core.DoorMonitor.Models
{
	public class StatisticsItem : ModelBase
    {
        public StatisticsItem()
        {
        }

        /// <summary>
        /// 获取或设置用户
        /// </summary>
        /// <value>The user.</value>
		public User User
		{
			get;
			set;
		}

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        /// <value>The unit.</value>
		public string Unit
		{
			get;
			set;
		}

        /// <summary>
        /// 获取或设置时长
        /// </summary>
        /// <value>The time.</value>
		public int Time
		{
			get;
			set;
		}

        /// <summary>
        /// 获取或设置进场时间
        /// </summary>
        /// <value>The time in.</value>
		public DateTime TimeIn
		{
			get;
			set;
		}

        /// <summary>
        /// 获取或设置出场时间
        /// </summary>
        /// <value>The time out.</value>
		public DateTime TimeOut
		{
			get;
			set;
		}
    }
}
