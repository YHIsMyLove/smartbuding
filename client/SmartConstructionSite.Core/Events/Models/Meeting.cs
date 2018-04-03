using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Events.Models
{
    [DataContract]
    public class Meeting : ModelBase
    {
        [DataMember]
        public string MeetingName { get; set; }
        //主持人
        [DataMember]
        public string Compere { get; set; }
        [DataMember]
        public DateTime MeetingCreatedAt { get; set; }

        //与当前用户相关的会议内容数量
        [DataMember]
        public int RelationalCount { get; set; }

        public override string ToString()
        {
            return MeetingName ?? base.ToString();
        }
    }
}
