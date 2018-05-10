using System;
using SmartConstructionSite.Core.Common;

namespace SmartConstructionSite.Core.DoorMonitor.Models
{
	/// <summary>
    /// 考勤统计
    /// </summary>
	public class AttendanceStatistics : ModelBase
    {
		public AttendanceStatistics()
        {
        }

        /// <summary>
        /// 获取或设置应出场人数
        /// </summary>
        /// <value>The total.</value>
		public int Total
		{
			get;
			set;
		}

		/// <summary>
        /// 获取或设置实出场人数
        /// </summary>
        /// <value>The total.</value>
        public int Real
        {
            get;
            set;
        }
        
		/// <summary>
        /// 获取或设置出勤正常人数
        /// </summary>
        /// <value>The total.</value>
        public int Normal
        {
            get;
            set;
        }

		/// <summary>
        /// 获取或设置迟到人数
        /// </summary>
        /// <value>The total.</value>
        public int Tardy
        {
            get;
            set;
        }
        
		/// <summary>
        /// 获取或设置早退人数
        /// </summary>
        /// <value>The total.</value>
        public int LeaveEarly
        {
            get;
            set;
        }

		/// <summary>
        /// 获取或设置缺勤人数
        /// </summary>
        /// <value>The total.</value>
		public int Absenteeism
        {
            get;
            set;
        }
    }
}
