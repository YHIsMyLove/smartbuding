using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartConstructionSite.Core.ProjectManagement.Models
{
    [DataContract]
    public class Province : ModelBase
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Province Prov { get; set; }
        [DataMember]
        public City City { get; set; }

		public override string ToString()
		{
            return Name == null ? base.ToString() : Name;
		}
	}
}
