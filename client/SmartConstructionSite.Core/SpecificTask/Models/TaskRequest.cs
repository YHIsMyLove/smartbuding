using System;
namespace SmartConstructionSite.Core.SpecificTask.Models
{
    /// <summary>
    /// 特殊作业申请类
    /// </summary>
    public class TaskRequest
    {
        public enum States
        {
            /// <summary>
            /// 已通过
            /// </summary>
            Passed,
            /// <summary>
            /// 待审核
            /// </summary>
            CheckPending,
            /// <summary>
            /// 已拒绝
            /// </summary>
            Refused
        }

        public TaskRequest()
        {
            State = States.CheckPending;
            Time = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置申请类型图标
        /// </summary>
        /// <value>The icon.</value>
        public string Icon
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置申请的名字
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置申请单位
        /// </summary>
        /// <value>The unit.</value>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置申请时间
        /// </summary>
        /// <value>The time.</value>
        public DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置申请状态
        /// </summary>
        /// <value>The state.</value>
        /// <see cref="States"/>
        public States State
        {
            get;
            set;
        }
    }
}
