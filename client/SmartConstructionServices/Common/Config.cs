using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Common
{
    public class Config
    {
        public static string host = "http://118.89.237.172:3000/";
        /// <summary>
        /// 用户登录（Post），参数：UserID=?&SserPwd=?
        /// </summary>
        public static string loginUrl = host + "api/Login";
        /// <summary>
        /// 获取用户信息（get），参数：SessionID=?
        /// </summary>
        public static string getUserInfoUrl = host + "api/GetUser?SessionID={0}";
        /// <summary>
        /// 检查session（Post），参数：SessionID=?
        /// </summary>
        public static string checkSessionUrl = host + "api/CheckSession";
        /// <summary>
        /// 获取部门列表（get），参数：ProjectID=?
        /// </summary>
        public static string getDepartmentsUrl = host + "api/GetDeptByProjID";
        /// <summary>
        /// 获取省份列表（get）
        /// </summary>
        public static string getProvincesUrl = host + "api/GetProv";
        /// <summary>
        /// 获取城市列表（get），参数：ProvID=?
        /// </summary>
        public static string getCitiesUrl = host + "api/GetCityByProvID";
        /// <summary>
        /// 获取项目列表（get），参数：CityID=?
        /// </summary>
        public static string getProjectsByCityIDUrl = host + "api/GetProjByCityID";
        /// <summary>
        /// 获取项目列表（get），参数：ProvID=?
        /// </summary>
        public static string getProjectsByProvIDUrl = host + "api/GetProjByProvID";

        public static string getUsersByProjIDUrl = host + "api/GetUserByProjID";

        public static string getUsersByDeptIDUrl = host + "api/GetUserByDeptID";

        public static string getUsersByRoleIDUrl = host + "api/GetUserByRoleID";

        public static string getMeetingsUrl = host + "api/GetMettings";

        public static string getCamerasUrl = host + "api/GetYSDevs";
    }
}
