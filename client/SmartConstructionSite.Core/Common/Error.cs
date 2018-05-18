using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Common
{
    public class Error
    {
        public Exception Exception { get; set; }

        public int Code { get; set; }

        public string Description { get; set; }
    }
}
