using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionSite.Core.Common
{
    public class Config
    {
        public const string debugHost = "http://192.168.43.216:3000/api/";
        public const string releaseHost = "http://118.89.237.172:3000/api/";
        public const string host = releaseHost;
        /// <summary>
        /// 用户登录（Post），参数：UserID=?&UserPwd=?
        /// </summary>
        public const string loginUrl = host + "Login";
        /// <summary>
        /// 退出登录（Post），参数：SessionID=？
        /// </summary>
        public const string logoutUrl = host + "LogOut";
        /// <summary>
        /// 获取用户信息（get），参数：SessionID=?
        /// </summary>
        public const string getUserInfoUrl = host + "GetUser?SessionID={0}";
        /// <summary>
        /// 检查session（Post），参数：SessionID=?
        /// </summary>
        public const string checkSessionUrl = host + "CheckSession";
        /// <summary>
        /// 获取部门列表（get），参数：ProjectID=?
        /// </summary>
        public const string getDepartmentsUrl = host + "GetDeptByProjID";
        /// <summary>
        /// 获取与用户相关的省份列表（get），参数：UserID=?
        /// </summary>
        public const string getProvByUserUrl = host + "GetProvByUser?UserID={0}";
        /// <summary>
        /// 获取城市列表（get），参数：ProvID=?
        /// </summary>
        public const string getCitiesUrl = host + "GetCityByProvID?ProvID={0}";
        /// <summary>
        /// 获取项目列表（get），参数：CityID=?
        /// </summary>
        public const string getProjectsByCityIDUrl = host + "GetProjByCityID?CityID={0}";
        /// <summary>
        /// 获取项目列表（get），参数：ProvID=?
        /// </summary>
        public const string getProjectsByProvIDUrl = host + "GetProjByProvID?ProvID={0}";
        /// <summary>
        /// 获取用户相关的项目列表（get），参数：UserID=?
        /// </summary>
        public const string getProjsByUser = host + "GetProjByUser?UserID={0}";

        public const string getUsersByProjIDUrl = host + "GetUserByProjID?ProjID={0}";

        public const string getUsersByDeptIDUrl = host + "GetUserByDeptID?DeptID={0}";

        public const string getUsersByRoleIDUrl = host + "GetUserByRoleID";

        public const string getMeetingsUrl = host + "GetMettings";

        public const string getCamerasUrl = host + "GetYSDevs";
    }
}
