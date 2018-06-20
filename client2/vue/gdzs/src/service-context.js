var isLogin = false;
var accessToken;
var sessionID;
var currentUser;
var currentProject;
var currentProv;
var currentCity;
var selectedProject;
var currentContact;
var currentMeeting;
let debugHost = "http://192.168.43.216:3000/api/";
let releaseHost = "http://118.89.237.172:3000/api/";
let host = releaseHost;
let urls = {
    
    /// <summary>
    /// 用户登录（Post），参数：UserID:?&UserPwd:?
    /// </summary>
    loginUrl: host + "Login",
    /// <summary>
    /// 退出登录（Post），参数：SessionID:？
    /// </summary>
    logoutUrl: host + "LogOut",
    /// <summary>
    /// 获取用户信息（get），参数：SessionID:?
    /// </summary>
    getUserInfoUrl: host + "GetUser?SessionID={0}",
    /// <summary>
    /// 检查session（Post），参数：SessionID:?
    /// </summary>
    checkSessionUrl: host + "CheckSession",
    /// <summary>
    /// 获取部门列表（get），参数：ProjID:?
    /// </summary>
    getDepartmentsUrl: host + "GetDeptByProjID?ProjID={0}",
    /// <summary>
    /// 获取与用户相关的省份列表（get），参数：UserID:?
    /// </summary>
    getProvByUserUrl: host + "GetProvByUser?UserID={0}",
    /// <summary>
    /// 获取城市列表（get），参数：ProvID:?
    /// </summary>
    getCitiesUrl: host + "GetCityByProvID?ProvID={0}",
    /// <summary>
    /// 获取项目列表（get），参数：CityID:?
    /// </summary>
    getProjectsByCityIDUrl: host + "GetProjByCityID?CityID={0}",
    /// <summary>
    /// 获取项目列表（get），参数：ProvID:?
    /// </summary>
    getProjectsByProvIDUrl: host + "GetProjByProvID?ProvID={0}",
    /// <summary>
    /// 获取用户相关的项目列表（get），参数：UserID:?
    /// </summary>
    getProjsByUser: host + "GetProjByUser?UserID={0}",
    /// <summary>
    /// 获取指定项目的人员列表（get），参数：ProjID:?&isEdit:true
    /// </summary>
    getUsersByProjIDUrl: host + "GetUserByProjID?ProjID={0}&isEdit=false",
    /// <summary>
    /// 获取指定部门的人员列表（get），参数：DeptID:?
    /// </summary>
    getUsersByDeptIDUrl: host + "GetUserByDeptID?ProjID={0}&DeptID={1}&isEdit=false",
    /// <summary>
    /// todo
    /// </summary>
    getUsersByRoleIDUrl: host + "GetUserByRoleID",
    /// <summary>
    /// 获取会议列表（get），参数：UserID:?&ProjID:?
    /// </summary>
    getMeetingsUrl: host + "APP/GetMeetings?UserID={0}&ProjID={1}",
    /// <summary>
    /// 获取会议纪要列表（get），参数：UserID:?&MeetingID:?
    /// </summary>
    getMeetingMinutesUrl: host + "APP/GetMeetingContents?UserID={0}&MeetingID={1}",

    setMeetingReaded: host + "SetMeetingReaded?MeetingID={0}",

    getCamerasUrl: host + "GetYSDevs",

    //排行榜
    //排行榜列表（get），参数：
    getRankingListUrl: host + "",
    //获取红榜列表（get），参数：
    getRedRankingListUrl: host + "",
    //获取黑榜列表（get），参数：
    getBlackRankingListUrl: host + "",

    //特种作业审批
    //获取当前用户创建的特种作业申请列表（get），参数：
    getRequestListCreateByMeUrl: host + "",
    //获取待当前用户审批的特种作业申请列表（get），参数：
    getCheckListByMeUrl: host + "",
    //获取抄送给当前用户的特种作业申请
    getRequestListCopyToMeUrl: host + "",
    //创建动火作业申请
    createDhzyRequestUrl: host + "",
    //同意签字
    checkUrl: host + "",
    //拒绝签字
    refuseUrl: host + "",
    //撤销申请
    cancelRequest: host + "",
    //转交他人
    copyToOther: host + "",

    //门禁
    //统计（图表数据）（get）
    getStatistics: host + "",
    //统计列表（get）
    getStatisticsList: host + "",
    //各个门的实时数据（get）
    getDoorsState: host + "",

    //人员考勤表（get）
    getAttendanceSheet: host + "",

    //机械设备监测
    //获取设备位置（get），参数：
    getDeviceLocations: host + "",
    //获取设备状态（get），参数：
    getDeviceState: host + "",
    //获取设备详细信息（get），参数：
    getDeviceInfo: host + "",
}
export default
    {
        isLogin,
        accessToken,
        currentUser,
        currentProject,
        currentProv,
        currentCity,
        selectedProject,
        currentContact,
        currentMeeting,
        sessionID,
        urls,
    }