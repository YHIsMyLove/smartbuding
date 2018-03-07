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

        internal async Task<Result<IList<Project>>> FindProjects(string province, string city)
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<IList<Project>> result = new Result<IList<Project>>();
                result.Model = new List<Project>()
                {
                    new Project(){ Name = "黄石奥体中心建设项目" },
                    new Project(){ Name = "黄石新下路区立交桥建设项目" }
                };
                return result;
            });
        }

        internal async Task<Result<IList<string>>> FetchProvinces()
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<IList<string>> result = new Result<IList<string>>();
                result.Model = new List<string>()
                {
                    "湖北省", "湖南省"
                };
                return result;
            });
        }

        internal async Task<Result<IList<string>>> FetchCities(string province)
        {
            await Task.Delay(2000);
            return await Task.Run(() =>
            {
                Result<IList<string>> result = new Result<IList<string>>();
                if (province == "湖北省")
                    result.Model = new List<string>()
                    {
                        "武汉市", "黄冈市"
                    };
                else
                    result.Model = new List<string>()
                    {
                        "长沙市", "郴州市"
                    };
                return result;
            });
        }
    }
}
