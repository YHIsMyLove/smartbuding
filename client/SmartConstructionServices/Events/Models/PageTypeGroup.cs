using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Events.Models
{
    public class PageTypeGroup : List<Meeting>
    {
        public string Title { get; set; }

        public string ShortName { get; set; }

        public PageTypeGroup(string title, string shortName)
        {
            Title = title;
            ShortName = shortName;
        }

    }
}
