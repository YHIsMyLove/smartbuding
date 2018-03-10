using System;
using SmartConstructionServices.Common;

namespace SmartConstructionServices.Account.Models
{
    public class User : ModelBase
    {
        public User()
        {

        }

        public string UserID { get; set; }
        public string UserName { get; set; }
        public int UserSex { get; set; }
        public int UserAge { get; set; }
        public DateTime UserBirth { get; set; }
        public string UserAddr { get; set; }
        public string UserHeadImg { get; set; }
        public string UserPhoneNum { get; set; }
        public DateTime UserCreatedAt { get; set; }
        public string UserCardID { get; set; }
        public string[] UserProjs { get; set; }
        public string[] UserRoles { get; set; }
    }
}
