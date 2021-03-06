﻿using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Events.Models
{
    
    public class Meeting : ModelBase
    {
        
        public string MeetingName { get; set; }
        //主持人
        
        public string Compere { get; set; }
        
        public DateTime MeetingCreatedAt { get; set; }

        public bool IsReaded
        {
            get;
            set;
        }

        //与当前用户相关的会议内容数量
        
        public int RelationalCount { get; set; }

        public override string ToString()
        {
            return MeetingName ?? base.ToString();
        }
    }
}
