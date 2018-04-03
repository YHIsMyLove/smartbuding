using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartConstructionSite.Core.PeopleManagement.Models
{
    [DataContract]
    public class Department : ModelBase
    {
        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name == null ? base.ToString() : Name;
        }
    }
}
