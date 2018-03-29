using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Common
{
    public class Result<T>
    {
        public T Model { get; set; }

        public Error Error { get; set; }

        public bool HasError { get; set; }
    }
}
