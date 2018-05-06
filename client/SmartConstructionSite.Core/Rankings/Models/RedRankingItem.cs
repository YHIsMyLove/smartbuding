using System;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;

namespace SmartConstructionSite.Core.Rankings.Models
{
    /// <summary>
    /// Red ranking item.
    /// </summary>
    public class RedRankingItem : ModelBase
    {
        public RedRankingItem()
        {
            Time = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置拥有者
        /// </summary>
        /// <value>The owner.</value>
        public User Owner
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Detail
        {
            get;
            set;
        }

        public int Points
        {
            get;
            set;
        }

        public int Stars
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }
    }
}
