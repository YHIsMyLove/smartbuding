using SmartConstructionServices.PeopleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Events.Models
{
    public class Meeting
    {
        public string Name { get; set; }
        //主持人
        public string Compere { get; set; }

        public DateTime Time { get; set; }

        //与当前用户相关的会议内容数量
        public int RelationalContentCount { get; set; }
    }
}
