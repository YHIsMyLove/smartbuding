using SmartConstructionSite.Core.Account.Models;
using SmartConstructionSite.Core.Events.Models;
using SmartConstructionSite.Core.PeopleManagement.Models;
using SmartConstructionSite.Core.ProjectManagement.Models;
using System;
using System.Runtime.Serialization;

namespace SmartConstructionSite.Core.Common
{
    [DataContract]
    [KnownType(typeof(Project))]
    [KnownType(typeof(Meeting))]
    [KnownType(typeof(MeetingMinutes))]
    [KnownType(typeof(City))]
    [KnownType(typeof(User))]
    [KnownType(typeof(Province))]
    [KnownType(typeof(Department))]
    public abstract class ModelBase
    {
        [DataMember]
        public string _id
        {
            get;
            set;
        }
    }
}
