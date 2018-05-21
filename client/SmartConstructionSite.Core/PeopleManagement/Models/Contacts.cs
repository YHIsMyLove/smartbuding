using SmartConstructionSite.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.PeopleManagement.Models
{
    public class Contacts : ModelBase
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
    }
}
