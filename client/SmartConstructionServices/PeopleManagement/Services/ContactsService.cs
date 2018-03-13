using SmartConstructionServices.Common;
using SmartConstructionServices.PeopleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.PeopleManagement.Services
{
    public class ContactsService
    {
        public Task<Result<IList<Contacts>>> Find(string project, string department, int page = 1, int pageSize = 10)
        {
            return Task.Run(() =>
            {
                Result<IList<Contacts>> result = new Result<IList<Contacts>>();
                result.Model = new List<Contacts>()
                {
                    new Contacts(){ Name = "张三", Post = "部门经理", PhoneNumber = "13333567657" },
                    new Contacts(){ Name = "李四", Post = "造价师", PhoneNumber = "13333567657" },
                    new Contacts(){ Name = "王五", Post = "预算员", PhoneNumber = "13333567657" },
                    new Contacts(){ Name = "展昭", Post = "设计师", PhoneNumber = "13333567657" },
                };
                return result;
            });
        }
    }
}
