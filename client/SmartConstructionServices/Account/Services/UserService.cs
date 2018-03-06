using SmartConstructionServices.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Account.Services
{
    public class UserService
    {
        public async Task<User> Login(string username, string password)
        {
            return await Task.Run(() => { return new User(); });
        }

        public async Task<bool> Logout()
        {
            return await Task.Run(() => { return true; });
        }
    }
}
