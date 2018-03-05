using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartConstructionServices.ProjectManagement.Models;

namespace SmartConstructionServices.ProjectManagement.Services
{
    public class ProjectService
    {
        public ProjectService()
        {
            
        }

        internal async Task<IList<Project>> FindProjects(string province, string city)
        {
            List<Project> projects = null;
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                projects = new List<Project>()
                {
                    new Project(){ Name = "黄石奥体中心建设项目" },
                    new Project(){ Name = "黄石新下路区立交桥建设项目" }
                };
            });
            return projects;
        }

        internal async Task<IList<string>> FetchProvinces()
        {
            List<string> provinces = null;
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                provinces = new List<string>()
                {
                    "湖北省", "湖南省"
                };
            });
            return provinces;
        }

        internal async Task<IList<string>> FetchCities(string province)
        {
            List<string> cities = null;
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                if (province == "湖北省")
                    cities = new List<string>()
                    {
                        "武汉市", "黄冈市"
                    };
                else
                    cities = new List<string>()
                    {
                        "长沙市", "郴州市"
                    };
            });
            return cities;
        }
    }
}
