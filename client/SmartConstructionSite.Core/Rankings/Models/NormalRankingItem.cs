using System;
using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Common;

namespace SmartConstructionSite.Core.Rankings.Models
{
    /// <summary>
    /// 表示一个名次
    /// </summary>
    public class NormalRankingItem : ModelBase
    {
        public NormalRankingItem()
        {
        }

        /// <summary>
        /// 获取或设置排名拥有者
        /// </summary>
        /// <value>The owner.</value>
        public User Owner
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置积分
        /// </summary>
        /// <value>The points.</value>
        public int Points
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置星星数量
        /// </summary>
        /// <value>The stars.</value>
        public int Stars
        {
            get;
            set;
        }
    }
}
