using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartConstructionSite.Core.ProjectManagement.Models
{
    public class City : ModelBase
    {
        public string Name { get; set; }

		public override string ToString()
		{
            return Name == null ? base.ToString() : Name;
		}
	}
}
