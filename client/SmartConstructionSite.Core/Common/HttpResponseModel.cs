using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartConstructionSite.Core.Common
{
    public class Stat
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public JObject data { get; set; }
        public string meta { get; set; }
    }
}
