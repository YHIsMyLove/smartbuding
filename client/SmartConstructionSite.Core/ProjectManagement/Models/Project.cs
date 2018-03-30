using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.ProjectManagement.Models
{
    public class Project : ModelBase
    {
        public string Name { get; set; }

		public override string ToString()
		{
            return Name == null ? base.ToString() : Name;
		}
	}
}
