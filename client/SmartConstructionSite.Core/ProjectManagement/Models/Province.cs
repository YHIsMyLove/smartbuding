using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartConstructionSite.Core.ProjectManagement.Models
{
    public class Province : ModelBase
    {
        public string Name { get; set; }

        public Province Prov { get; set; }

        public City City { get; set; }

		public override string ToString()
		{
            return Name == null ? base.ToString() : Name;
		}
	}
}
