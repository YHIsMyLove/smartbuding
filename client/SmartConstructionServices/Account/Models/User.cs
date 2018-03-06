using System;
using SmartConstructionServices.Common;

namespace SmartConstructionServices.Account.Models
{
    public class User : ModelBase
    {
        public User()
        {
            
        }

        public string Username { get; set; }

        public string PhotoPath { get; set; }
    }
}
