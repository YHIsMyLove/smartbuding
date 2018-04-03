using SmartConstructionSite.Core.Common;
using System;
using System.Runtime.Serialization;

namespace SmartConstructionSite.Core.Account.Models
{
    [DataContract]
    public class User : ModelBase
    {
        public User()
        {

        }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int UserSex { get; set; }
        [DataMember]
        public int UserAge { get; set; }
        [DataMember]
        public string UserHeadImg { get; set; }
        [DataMember]
        public string UserPhoneNum { get; set; }
        //public DateTime UserCreatedAt { get; set; }
        [DataMember]
        public string UserCardID { get; set; }
        //public string Department { get; set; }
        //public string Position { get; set; }
        [DataMember]
        public string UserEmail { get; set; }
        [DataMember]
        public string[] DeptNames { get; set; }
        /// <summary>
        /// 证书图像地址数组
        /// </summary>
        [DataMember]
        public string[] Certificates { get; set; }
    }
}
