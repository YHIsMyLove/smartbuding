﻿using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace SmartConstructionSite.Core.Account.Models
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
        
        public string UserHeadImg { get; set; }
        
        public string UserPhoneNum { get; set; }
        //public DateTime UserCreatedAt { get; set; }
        
        public string UserCardID { get; set; }
        //public string Department { get; set; }
        //public string Position { get; set; }
        
        public string UserEmail { get; set; }
        
        public string[] DeptNames { get; set; }
        /// <summary>
        /// 证书图像地址数组
        /// </summary>
        public IList<string> Certificates { get; set; }

		public string WorkNumber
		{
			get;
			set;
		}
    }
}
