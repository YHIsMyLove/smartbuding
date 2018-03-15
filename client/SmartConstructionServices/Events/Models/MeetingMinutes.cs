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

        public IList<string> ExecuteDepartments { get; set; }
    }
}
