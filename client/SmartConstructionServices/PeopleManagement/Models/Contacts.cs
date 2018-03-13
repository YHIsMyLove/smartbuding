using SmartConstructionServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.PeopleManagement.Models
{
    public class Contacts : ModelBase
    {
        public string Name { get; set; }
        public string Post { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
    }
}
