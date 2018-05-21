using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Events.Models
{
    public abstract class Event : ModelBase
    {
        public string Desription { get; set; }
        public DateTime Time { get; set; }
    }
}
