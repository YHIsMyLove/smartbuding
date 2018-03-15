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
                    new Contacts(){ Name = "张三", Department = "市场部", Position = "部门经理", PhoneNumber = "13333567657", PhotoUrl = "http://himg.bdimg.com/sys/portrait/item/31bd6c62676f6e6766752b0e.jpg" },
                    new Contacts(){ Name = "李四", Department = "市场部", Position = "造价师", PhoneNumber = "13333567657", PhotoUrl = "http://himg.bdimg.com/sys/portrait/item/31bd6c62676f6e6766752b0e.jpg" },
                    new Contacts(){ Name = "王五", Department = "市场部", Position = "预算员", PhoneNumber = "13333567657", PhotoUrl = "http://himg.bdimg.com/sys/portrait/item/31bd6c62676f6e6766752b0e.jpg" },
                    new Contacts(){ Name = "展昭", Department = "市场部", Position = "设计师", PhoneNumber = "13333567657", PhotoUrl = "http://himg.bdimg.com/sys/portrait/item/31bd6c62676f6e6766752b0e.jpg" },
                };
                return result;
            });
        }
    }
}
