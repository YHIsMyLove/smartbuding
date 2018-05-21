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
        /// 获取或设置进场人次
        /// </summary>
        /// <value>The jin chang.</value>
		public int Jc
		{
			get;
			set;
		}

        /// <summary>
        /// 获取和设置出场人数
        /// </summary>
        /// <value>The leave.</value>
		public int Cc
		{
			get;
			set;
		}
        
        /// <summary>
        /// 获取或设置在场人数
        /// </summary>
        /// <value>The zai chang.</value>
        public int Zc
		{
			get;
			set;
		}

        /// <summary>
        /// 获取或设置现场人数
        /// </summary>
        /// <value>The xc.</value>
		public int Xc
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置应出勤人数
		/// </summary>
		/// <value>The total.</value>
		public int Ycq
		{
			get;
			set;
		}

		/// <summary>
        /// 获取或设置实出勤人数
        /// </summary>
        /// <value>The total.</value>
        public int Scq
        {
            get;
            set;
        }
        
		/// <summary>
        /// 获取或设置出勤正常人数
        /// </summary>
        /// <value>The total.</value>
        public int Cqzc
        {
            get;
            set;
        }

		/// <summary>
        /// 获取或设置迟到人数
        /// </summary>
        /// <value>The total.</value>
        public int Cd
        {
            get;
            set;
        }
        
		/// <summary>
        /// 获取或设置早退人数
        /// </summary>
        /// <value>The total.</value>
        public int Zt
        {
            get;
            set;
        }

		/// <summary>
        /// 获取或设置缺勤人数
        /// </summary>
        /// <value>The total.</value>
		public int Qq
        {
            get;
            set;
        }
    }
}
