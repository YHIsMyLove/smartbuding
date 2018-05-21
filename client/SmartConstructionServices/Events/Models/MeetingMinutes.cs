using SmartConstructionServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Events.Models
{
    public class MeetingMinutes : ModelBase
    {
        public string Content { get; set; }

        public string ExecuteDepartments { get; set; }

        /// <summary>
        /// 是否与当前用户相关
        /// </summary>
        public bool IsRelational { get; set; }
    }
}
