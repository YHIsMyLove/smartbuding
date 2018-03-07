﻿using SmartConstructionServices.Account.Models;
using SmartConstructionServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Account.Services
{
    public class UserService
    {
        public async Task<Result<User>> Login(string username, string password)
        {
            return await Task.Run(() =>
            {
                Result<User> result = new Result<User>();
                result.Model = new User();
                return result;
            });
        }

        public async Task<Result<bool>> Logout()
        {
            return await Task.Run(() =>
            {
                Result<bool> result = new Result<bool>();
                result.Model = true;
                return result;
            });
        }

        public async Task<Result<User>> CheckSession()
        {
            return await Task.Run(() =>
            {
                Result<User> result = new Result<User>();
                result.Model = new User();
                return result;
            });
        }
    }
}
