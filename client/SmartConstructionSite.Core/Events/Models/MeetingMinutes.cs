using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.PeopleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Events.Models
{
    [DataContract]
    public class MeetingMinutes : ModelBase
    {
        private IList<Department> depts;

        [DataMember]
        public string MeetingID { get; set; }
        [DataMember]
        public string MeetingTitle { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public IList<Department> Depts
        {
            get { return depts; }
            set
            {
                depts = value;
                if (depts != null && depts.Count > 0)
                {
                    foreach (var item in depts)
                    {
                        DepartmentInfo += item.Name + " ";
                    }
                }
            }
        }

        public string DepartmentInfo { get; private set; }

        /// <summary>
        /// 是否与当前用户相关
        /// </summary>
        /// 
        [DataMember]
        public bool IsRelational { get; set; }

        public override string ToString()
        {
            return Content ?? base.ToString();
        }
    }
}
