using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartConstructionServices.ProjectManagement.Models;
using SmartConstructionServices.Common;

namespace SmartConstructionServices.ProjectManagement.Services
{
    public class ProjectService
    {
        public ProjectService()
        {
            
        }

        internal async Task<Result<IList<string>>> FindProjects(string province, string city)
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<IList<string>> result = new Result<IList<string>>();
                result.Model = SimpleData.Instance.GetProjects(province, city);
                return result;
            });
        }

        internal async Task<Result<IList<string>>> FetchProvinces()
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<IList<string>> result = new Result<IList<string>>();
                result.Model = SimpleData.Instance.GetProvinces();
                return result;
            });
        }

        internal async Task<Result<IList<string>>> FetchCities(string province)
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<IList<string>> result = new Result<IList<string>>();
                result.Model = SimpleData.Instance.GetCities(province);
                return result;
            });
        }
    }
}
