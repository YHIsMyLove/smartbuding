const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const YS = require('../../utils/yingshi')
const Qcos = require('../../utils/Qcos.js')
const moment = require('moment')
const multiparty = require('multiparty')
const fs = require('fs')
const ImageMagick = require('../../utils/ImageMagick')

const UserSession = mongoose.model('UserSession')
const SysTable = mongoose.model('SysTable')
const User = mongoose.model('User')
const UserDept = mongoose.model('UserDept')
const UserProj = mongoose.model('UserProj')
const UserRole = mongoose.model('UserRole')
const Device = mongoose.model('Device')
const Metting = mongoose.model('Metting')
const MeetingMinutes = mongoose.model('MeetingMinutes')

/****************************************************************** */
/**系统登录***********************************************************/
/****************************************************************** */
//登录
exports.Login = async (req, res) => {
    var query = {
        UserID: req.body.UserID,
        UserPwd: req.body.UserPwd
    }
    console.log(query)
    if (query.UserID == 'admin') {
        if ((query.UserID === SystemConfig.Admin_User) &&
            (query.UserPwd === SystemConfig.Admin_Pwd)) {
            return res.send(msg.genSuccessMsg('登录成功'))
        }
        return res.send(msg.genFailedMsg('登录失败'))
    } else {
        try {
            //1. 查询session
            var sessionid = ''
            let session = await UserSession.findOne({ UserID: query.UserID }).exec()
            if (session)
                sessionid = session._id

            //2. 匹配账号密码
            let _user = await User.findOne({ UserID: query.UserID }).exec()
            console.log(_user)
            if (!_user) return res.send(msg.genFailedMsg('该账号不存在!'))
            if (_user.UserPwd != query.UserPwd) return res.send(msg.genFailedMsg('密码错误!'))

            //2. 查询匹配项目 若没有则不允许登录
            let haveProj = await UserProj.findOne({ UserID: _user._id })
            if (!haveProj) {
                return res.send(msg.genFailedMsg('没权限登录'))
            }
            //3. 将数据存进/更新session表
            if (sessionid == '') {
                let _usersession = new UserSession({
                    UserID: _user.UserID
                })
                await _usersession.updateAndSave()
                sessionid = _usersession._id
                console.log(sessionid)
            }
            //4. 取出萤石的token
            let ys_result = await YS.getaccessToken()
            let ystoken = ys_result.data.code == '200' ? ys_result.data.data.accessToken : ''

            return res.send(msg.genSuccessMsg('登录成功', {
                UserID: query.UserID,
                SessionID: sessionid,
                YSToken: ystoken
            }))
        } catch (error) {
            return res.send(msg.genFailedMsg('未知错误!' + error))
        }
    }
}

//检查用户session
exports.checkSession = async (req, res) => {
    let query = {
        _id: req.body.SessionID
    }
    let session = await UserSession.findOne(query).exec()
    if (session) {
        return res.send(msg.genSuccessMsg('登录成功'))
    }
    res.send(msg.genFailedMsg('无效session,请登录'))
}

/**
 * 注销 POST SessionID
 */
exports.LogOut = async (req, res) => {
    let query = {
        _id: req.body.SessionID
    }
    try {
        await UserSession.findOne(query).remove()
        res.send(msg.genSuccessMsg('注销成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('注销失败', error))
    }
}

/**
 * 获取用户信息 Get for Login over
 * @param {*} req.query.SessionID  
 * @param {*} res 
 */
exports.getUserInfo = async (req, res) => {
    let query = {
        _id: req.query.SessionID
    }
    let session = await UserSession.findOne(query).exec()
    if (session) {
        let id = session.UserID
        //1. 获取用户
        let user = await User.findOne({ UserID: id }).exec()
        if (user) {
            //2. 获取部门
            let userdepts_result = await UserDept.find({ UserID: user._id }).select('DeptID')
            if (userdepts_result) {
                let depts_result = await SysTable.find({ SysFieldID: "dept", _id: { $in: userdepts_result.map(i => i.DeptID) } })
                user.Depts = depts_result.map(i => i.item0)
                console.log(user)
            }

            //4. 获取角色
            return res.send(msg.genSuccessMsg('获取成功!', {
                _id: user._id,
                UserID: user.UserID,
                UserName: user.UserName,
                UserSex: user.UserSex,
                UserAge: user.UserAge,
                UserPhoneNum: user.UserPhoneNum,
                UserEmail: user.UserEmail,
                UserCardID: user.UserCardID,
                DeptNames: user.Depts
            }))
        }
    }
    res.send(msg.genFailedMsg('无效session,请登录'))
}

/**
 * 获取用户相关的省份
 * @param {*} req 
 * @param {*} res 
 */
exports.GetProvByUser = async (req, res) => {
    let query = {
        UserID: req.query.UserID
    }
    //1. 取得项目ID
    let proj = await UserProj.find(query).select('ProjID').exec()
    let projids = proj.map(i => i.ProjID)
    //2. 取得所有市区
    let cityids = await SysTable.find({ _id: { $in: projids } }).select('item1')
    let ids = cityids.map(i => i.item1)
    let cityinfos = await SysTable.find({ SysFieldID: 'city', _id: { $in: ids } })
    let provids = cityinfos.map(i => i.item1)
    //3. 取得所有省份
    let provs = await SysTable.find({ SysFieldID: 'province', _id: { $in: provids } })
    res.send(msg.genSuccessMsg('查询成功',
        provs.map(i => {
            return {
                Name: i.item0,
                _id: i._id,
            }
        })
    ))
}

/**
 * 根据用户获取相关项目
 */
exports.GetProjByUser = async (req, res) => {
    let query = {
        UserID: req.query.UserID
    }
    //1. 取得项目ID
    let proj = await UserProj.find(query).select('ProjID').exec()
    let projids = proj.map(i => i.ProjID)
    //3. 取得所有项目
    let projs = await SysTable.find({ SysFieldID: 'proj', _id: { $in: projids } })
    res.send(msg.genSuccessMsg('查询成功',
        projs.map(i => {
            return {
                Name: i.item0,
                _id: i._id,
            }
        })
    ))
}


//log out 注销
/****************************************************************** */
/**用户项目管理*******************************************************/
/****************************************************************** */

//获取项目中的人员信息
exports.GetUserByProjID = async (req, res) => {
    let query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit),
    }
    //to do..............................
    let isEdit = req.query.isEdit
    let ProjID = req.query.ProjID
    let count = query.limit
    if (isEdit == 'true' && ProjID != -1) {// 
        console.log('编辑模式且有项目ID')
        let list = await User.find({})
            .sort({ createdAt: -1 })
            .limit(query.limit)
            .skip(query.limit * query.page)
            .exec();
        let ids = list.map(i => i._id.toString())
        let userprojlist = await UserProj.find({ UserID: { $in: ids }, ProjID: ProjID }).exec()//
        //group user and userprojlist
        let data = list.map(i => {
            let r = {
                _id: i._id,
                UserID: i.UserID,
                UserPwd: i.UserPwd,
                UserName: i.UserName,
                UserSex: i.UserSex,
                UserAge: i.UserAge,
                UserPhoneNum: i.UserPhoneNum,
                UserInProj: userprojlist.find(d => d.UserID == i._id) != undefined
            }
            return r
        })
        count = await User.count()
        return res.send(msg.genSuccessMsg("查询成功", data, { count: count }))
    }

    if (isEdit == 'true' && ProjID == -1) {
        console.log('编辑模式项目ID=-1')
        let list = await User.find({})
            .sort({ createdAt: -1 })
            .limit(query.limit)
            .skip(query.limit * query.page)
            .exec();
        count = await User.count()
        return res.send(msg.genSuccessMsg("查询成功", list, { count: count }))
    }

    //
    if (isEdit == 'false' && ProjID != -1) {
        console.log('非编辑模式且有项目ID')
        try {
            let result = await UserProj.find({ ProjID: ProjID }).select('UserID')
            let q = result.map(i => {
                return {
                    "_id": i.UserID
                }
            })
            let users = await User.find({ _id: { $in: q } })
                .sort({ createdAt: -1 })
                .limit(query.limit)
                .skip(query.limit * query.page)
                .exec()
            count = await User.find({ _id: { $in: q } }).count()

            res.send(msg.genSuccessMsg("查询成功", users, { count: count }))
        } catch (error) {
            res.send(msg.genFailedMsg("查询失败"))
        }
    }
    res.send(msg.genFailedMsg("查询失败"))
}

//设置用户项目表
exports.InsertOrDelUserProj = async (req, res) => {
    let insertOrDel = req.body.insertOrDel
    let query = {
        UserID: req.body.UserID,
        ProjID: req.body.ProjID,
    }
    if (!query.UserID) {
        return res.send(msg.genFailedMsg('请输入人员ID'))
    }
    if (!query.ProjID) {
        return res.send(msg.genFailedMsg('请输入项目ID'))
    }
    try {
        if (insertOrDel == 'insert') {
            let _userProj = new UserProj(query)
            await _userProj.updateAndSave()
            res.send(msg.genMsg('保存成功'))
        } else {
            await UserProj.findOne(query).remove()
            res.send(msg.genMsg('删除成功'))
        }
    } catch (err) {
        res.send(msg.genFailedMsg('保存失败', err))
    }
}

/****************************************************************** */
/**用户部门管理*******************************************************/
/****************************************************************** */

//根据项目ID获取所有的部门数据
exports.GetDeptTreeByProjID = async (req, res) => {
    var query = {
        SysFieldID: "dept",
        item2: req.query.ProjID//自定义表中的ITem2代表项目ID
    }
    try {
        if (!query) return res.send(msg.genFailedMsg('请输入项目ID'))
        let result = await SysTable.find(query)

        //映射
        let data = result.map(i => {
            return {
                ID: i._id,
                DeptName: i.item0,
                ParentID: i.item1,
                ProjID: i.item2,
            }
        })

        //组装数据
        let realdata = []
        //根节点
        let root1 = data.filter(i => i.ParentID == 'undefined')[0]
        let root = {
            ID: root1.ID,
            label: root1.DeptName,
            children: []
        }
        realdata.push(root)
        let recursion = (list, root) => {
            list.forEach(item => {
                if (item.ParentID == root.ID) {
                    if (!root.children) root.children = []
                    root.children.push({
                        ID: item.ID,
                        label: item.DeptName
                    })
                    recursion(list, root.children.filter(i => i.ID == item.ID)[0])
                }
            });
        }
        recursion(data, root)

        if (result.length == 0) return res.send(msg.genFailedMsg('没有获取到部门数据,请检查项目ID'))
        res.send(msg.genMsg("获取成功", "", realdata))
    } catch (error) {
        return res.send(msg.genFailedMsg('未知错误'))
    }
}

//根据项目ID获取所有的部门数据
exports.GetDeptByProjID = async (req, res) => {
    var query = {
        SysFieldID: "dept",
        item2: req.query.ProjID//自定义表中的ITem2代表项目ID
    }
    try {
        if (!query) return res.send(msg.genFailedMsg('请输入项目ID'))
        let result = await SysTable.find(query)

        let data = result.filter(i => i.item0 != 'Root').map(i => {
            return {
                label: i.item0,
                value: { id: i._id, type: "dept" },
                children: []
            }
        })
        res.send(msg.genSuccessMsg("获取成功", data))
    } catch (error) {
        return res.send(msg.genFailedMsg('未知错误'))
    }
}

//设置用户部门表
exports.InsertOrDelUserDept = async (req, res) => {
    let insertOrDel = req.body.insertOrDel
    if (!insertOrDel) {
        return res.send(msg.genFailedMsg('请输入插入/删除'))
    }
    let query = {
        UserID: req.body.UserID,
        ProjID: req.body.ProjID,
        DeptID: req.body.DeptID,
    }
    if (!query.UserID) {
        return res.send(msg.genFailedMsg('请输入人员ID'))
    }
    if (!query.ProjID) {
        return res.send(msg.genFailedMsg('请输入项目ID'))
    }
    if (!query.DeptID) {
        return res.send(msg.genFailedMsg('请输入部门ID'))
    }
    try {
        if (insertOrDel == 'insert') {
            let _userDept = new UserDept(query)
            await _userDept.updateAndSave()
            res.send(msg.genMsg('保存成功'))
        } else {
            await UserDept.findOne(query).remove()
            res.send(msg.genMsg('删除成功'))
        }
    } catch (err) {
        res.send(msg.getFailedMsg('保存失败'))
    }
}

//获取省份资料
exports.GetProv = async (req, res) => {
    try {
        let prov = await SysTable.find({ SysFieldID: "province" })
        let result = prov.map(i => {
            return {
                label: i.item0,
                value: { id: i._id, type: "prov" },
                children: []
            }
        })
        res.send(msg.genMsg('获取成功', "", result))
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败"))
    }
}

//获取城市信息
exports.GetCityByProvID = async (req, res) => {
    let query = {
        item1: req.query.ProvID,
        SysFieldID: "city"
    }
    if (!query.item1) return res.send(msg.genFailedMsg('请输入省份ID'))
    try {
        let city = await SysTable.find(query)
        let result = city.map(i => {
            return {
                label: i.item0,
                value: { id: i._id, type: "city" }
            }
        })
        res.send(msg.genMsg('获取成功', "", result))
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败"))
    }
}

//根据城市获取项目信息
exports.GetProjByCityID = async (req, res) => {
    let query = {
        item1: req.query.item1,
        SysFieldID: "proj"
    }
    if (!query.item1) return res.send(msg.genFailedMsg('请输入城市ID'))
    try {
        let proj = await SysTable.find(query)
        let result = proj.map(i => {
            return {
                label: i.item0,
                value: { id: i._id, type: "proj" },
            }
        })
        res.send(msg.genMsg('获取成功', "", result))
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败"))
    }
}

//根据部门获取人员信息
exports.GetUserByDeptID = async (req, res) => {
    let query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit),
    }
    let isEdit = req.query.isEdit
    let DeptID = req.query.DeptID
    let ProjID = req.query.ProjID
    let count = query.limit

    if (isEdit == 'true' && DeptID != -1) {// 
        console.log('编辑模式且有部门ID')
        let list = await User.find({})
            .sort({ createdAt: -1 })
            .limit(query.limit)
            .skip(query.limit * query.page)
            .exec();
        let ids = list.map(i => i._id.toString())
        let userinproj = await UserProj.find({ UserID: { $in: ids }, ProjID: ProjID })
        let userdeptlist = await UserDept.find({ UserID: { $in: ids }, DeptID: DeptID, ProjID: ProjID })
        //group user and userprojlist
        let data = list
            .filter(item => userinproj.filter(p => p.UserID == item._id).length > 0)
            .map(i => {
                let r = {
                    _id: i._id,
                    UserID: i.UserID,
                    UserPwd: i.UserPwd,
                    UserName: i.UserName,
                    UserSex: i.UserSex,
                    UserAge: i.UserAge,
                    UserPhoneNum: i.UserPhoneNum,
                    UserInDept: userdeptlist.find(d => d.UserID == i._id) != undefined
                }
                return r
            })
        count = await User.count()
        return res.send(msg.genSuccessMsg("查询成功", data, { count: count }))
    }

    if (isEdit == 'false' && DeptID != -1) {
        console.log('非编辑模式且有部门ID')
        try {
            let result = await UserDept.find({ ProjID: ProjID, DeptID: DeptID }).select('UserID')
                .limit(query.limit)
                .skip(query.limit * query.page)
            count = await UserDept.find({ ProjID: ProjID, DeptID: DeptID }).count()

            let q = result.map(i => {
                return {
                    "_id": i.UserID
                }
            })
            let users = await User.find({ _id: { $in: q } })
                .sort({ createdAt: -1 })
                .exec()
            console.log(users)
            return res.send(msg.genSuccessMsg("查询成功", users, { count: count }))
        } catch (error) {
            return res.send(msg.genFailedMsg("查询失败"))
        }
    }
    res.send(msg.genFailedMsg("查询失败"))
}

/**
 * 根据省份获取项目信息
 */
exports.GetProjByProvID = async (req, res) => {
    let query = {
        item1: req.query.item1,
        SysFieldID: "proj"
    }
    if (!query.item1) return res.send(msg.genFailedMsg('请输入省份ID'))
    try {
        let proj = await SysTable.find(query)
        let result = proj.map(i => {
            return {
                label: i.item0,
                value: { id: i._id, type: "proj" },
            }
        })
        res.send(msg.genMsg('获取成功', "", result))
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败"))
    }
}

/****************************************************************** */
/**用户角色管理*******************************************************/
/****************************************************************** */
//获取角色资料
exports.GetRoleByRoleID = async (req, res) => {
    let query = {
        SysFieldID: 'role',
        item2: req.query.ProjID
    }
    try {
        let role = await SysTable.find(query)
        let result = role.map(i => {
            return {
                label: i.item0,
                value: i._id,
            }
        })
        res.send(msg.genSuccessMsg('获取成功', result))
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败->" + error))
    }
}

//根据角色获取人员信息
exports.GetUserByRoleID = async (req, res) => {
    let query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit),
    }
    let isEdit = req.query.isEdit
    let RoleID = req.query.RoleID
    let ProjID = req.query.ProjID
    let count = query.limit

    if (isEdit == 'true' && RoleID != -1) {// 
        console.log('编辑模式且有角色ID')
        let list = await User.find({})
            .sort({ createdAt: -1 })
            .limit(query.limit)
            .skip(query.limit * query.page)
            .exec();
        let ids = list.map(i => i._id.toString())
        let userrolelist = await UserRole.find({ UserID: { $in: ids }, RoleID: RoleID, ProjID: ProjID }).exec()//
        let userinproj = await UserProj.find({ UserID: { $in: ids }, ProjID: ProjID })
        //group user and userprojlist
        let data = list
            .filter(item => userinproj.filter(p => p.UserID == item._id).length > 0)
            .map(i => {
                let r = {
                    _id: i._id,
                    UserID: i.UserID,
                    UserPwd: i.UserPwd,
                    UserName: i.UserName,
                    UserSex: i.UserSex,
                    UserAge: i.UserAge,
                    UserPhoneNum: i.UserPhoneNum,
                    UserInRole: userrolelist.find(d => d.UserID == i._id) != undefined
                }
                return r
            })
        count = await User.count()
        return res.send(msg.genSuccessMsg("查询成功", data, { count: count }))
    }

    if (isEdit == 'true' && RoleID == -1) {
        console.log('编辑模式角色ID=-1')
        let list = await User.find({})
            .sort({ createdAt: -1 })
            .limit(query.limit)
            .skip(query.limit * query.page)
            .exec();
        count = await User.count()
        return res.send(msg.genSuccessMsg("查询成功", list, { count: count }))
    }
    //
    if (isEdit == 'false' && RoleID != -1) {
        console.log('非编辑模式且有角色ID')
        console.log(ProjID + '**********' + RoleID)
        try {
            let result = await UserRole.find({ ProjID: ProjID, RoleID: RoleID }).select('UserID')
            let q = result.map(i => {
                return {
                    "_id": i.UserID
                }
            })
            let users = await User.find({ _id: { $in: q } })
                .sort({ createdAt: -1 })
                .limit(query.limit)
                .skip(query.limit * query.page)
                .exec()
            count = await User.find({ _id: { $in: q } }).count()

            return res.send(msg.genSuccessMsg("查询成功", users, { count: count }))
        } catch (error) {
            return res.send(msg.genFailedMsg("查询失败"))
        }
    }
    res.send(msg.genFailedMsg("查询失败"))
}

//设置用户角色表
exports.InsertOrDelUserRole = async (req, res) => {
    let insertOrDel = req.body.insertOrDel
    if (!insertOrDel) {
        return res.send(msg.genFailedMsg('请输入插入/删除'))
    }
    let query = {
        UserID: req.body.UserID,
        ProjID: req.body.ProjID,
        RoleID: req.body.RoleID,
    }
    if (!query.UserID) {
        return res.send(msg.genFailedMsg('请输入人员ID'))
    }
    if (!query.ProjID) {
        return res.send(msg.genFailedMsg('请输入项目ID'))
    }
    if (!query.RoleID) {
        return res.send(msg.genFailedMsg('请输入角色ID'))
    }
    try {
        if (insertOrDel == 'insert') {
            let _userDept = new UserRole(query)
            await _userDept.updateAndSave()
            res.send(msg.genMsg('保存成功'))
        } else {
            await UserRole.findOne(query).remove()
            res.send(msg.genMsg('删除成功'))
        }
    } catch (err) {
        res.send(msg.getFailedMsg('保存失败'))
    }
}

/****************************************************************** */
/**设备管理***********************************************************/
/****************************************************************** */
exports.GetDevsByRole = async (req, res) => {
    let query = {
        ProjID: req.query.ProjID,
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit),
    }
    if (!query.ProjID) return res.send(msg.genFailedMsg('请输入项目ID'))
    let devs = await Device.find(query)

}

//获取萤石设备
exports.GetYSDevs = async (req, res) => {
    let query = {
        token: req.query.token
    }
    try {
        console.log(query.token)
        let cameralist = await YS.getCameraList(query)
        // let result = 
        if (cameralist.data.code == '200') {
            let result = cameralist.data.data
                .map(i => {
                    return {
                        DevID: i.deviceSerial,
                        DevName: i.deviceName,
                        DevStatus: i.status,
                        DevVersion: i.deviceVersion,
                        DevType: i.deviceType,
                    }
                })
            res.send(msg.genSuccessMsg('查询成功', result, { count: cameralist.data.page.total }))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('查询失败->' + error))
        console.log(error)
    }
}
/****************************************************************** */
/**会议***********************************************************/
/****************************************************************** */
exports.GetMettings = async (req, res) => {
    let query = {
        ProjID: req.query.ProjID,
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    console.log('获取会议')
    try {
        let result = await Metting.list(query)
        let data = result.map(i => {
            return {
                _id: i._id,
                MettingName: i.MettingName,
                MettingCreatedAt: moment(i.MettingCreatedAt).format('YYYY-MM-DD hh:mm:ss'),
                Compere: i.Compere
            }
        })
        let count = await Metting.count()
        console.log(count)
        res.send(msg.genSuccessMsg('查询成功', data, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('查询失败->' + error))
    }
}

//获取会议内容
exports.GetMeetingContentByMeetingID = async (req, res) => {
    let query = {
        MeetingID: req.query.MeetingID
    }
    try {
        let result = await MeetingMinutes.find(query)
        res.send(msg.genSuccessMsg('获取成功', result))
    } catch (error) {
        res.send(msg.genFailedMsg('获取失败' + error))
    }

}

//插入或修改会议信息
exports.InsertOrUpdateMetting = async (req, res) => {
    try {
        if (req.body._id == undefined) {
            let metting = new Metting(req.body)
            await metting.updateAndSave();
        } else {
            let metting = req.metting
            metting = Object.assign(Metting, req.body);
            await metting.updateAndSave();
        }
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('保存失败->' + error))
    }
}

//插入或修改会议内容
exports.InsertOrUpdateMeetingContent = async (req, res) => {
    try {
        console.log(req.body._id)
        if (req.body._id == '-1') {
            req.body._id = undefined
            let content = new MeetingMinutes(req.body)
            await content.updateAndSave();
        } else {
            console.log(req.body)
            await MeetingMinutes.updateOne({ _id: req.body._id }, req.body).exec()

        }
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('保存失败->' + error))
    }
    // try {
    //     if (req.body._id == undefined) {
    //         let metting = new Metting(req.body)
    //         await metting.updateAndSave();
    //     } else {
    //         let metting = req.metting
    //         metting = Object.assign(Metting, req.body);
    //         await metting.updateAndSave();
    //     }
    //     res.send(msg.genSuccessMsg('保存成功'))
    // } catch (error) {
    //     res.send(msg.genFailedMsg('保存失败->' + error))
    // }
}

//删除会议内容
exports.DelMettingContent = async (req, res) => {

}

/****************************************************************** */
/**文件上传***********************************************************/
/****************************************************************** */
//获取文件
exports.GetFiles = async (req, res) => {
    let query = {
        startKey: req.query.startKey,
        pageCount: req.query.pageCount
    }
    let result = await Qcos.GetBucketAsync(query)
    let count = await Qcos.GetBucketCount()
    if (result.error) {
        return res.send(msg.genFailedMsg('获取失败!' + result.error))
    }
    res.send(msg.genSuccessMsg("获取成功", result.data, { count: count - 1 }))
}

//上传文件
exports.UploadFile = (req, res) => {
    //生成multiparty对象，并配置上传目标路径
    var form = new multiparty.Form({ uploadDir: './static' });
    //上传完成后处理
    form.parse(req, (err, fields, files) => {
        if (err) {
            return res.send(msg.genFailedMsg('上传失败', err))
        }
        var inputFile = files.file[0];
        var uploadedPath = inputFile.path;
        var dstPath = './static/' + inputFile.originalFilename;
        fs.rename(uploadedPath, dstPath, async function (err) {
            if (err) {
                return res.send(msg.genFailedMsg('更名失败', err))
            }
            else {
                files.file.path = dstPath;
                let data = files;
                //1. 获得缩略图
                let resizeimg_result = await ImageMagick.ResizeImg(dstPath)
                if (!resizeimg_result.err) {
                    Qcos.uploadSync(resizeimg_result.data, (err, qcospath) => {
                        if (err) {
                            return res.send(msg.genFailedMsg('上传失败', err))
                        }
                        res.send(msg.genSuccessMsg('上传成功', qcospath))

                        //2. 删除本地
                        fs.unlinkSync(resizeimg_result.data)
                        //3. 删除原图
                        fs.unlinkSync(files.file.path)
                    })
                } else {
                    //缩略图失败..
                    return res.send(msg.genFailedMsg('上传失败', err))
                }
            }
        });
    });
}

/**
 * 根据索引取得对象名称
 * @param {*} req 
 * @param {*} res 
 */
exports.GetFileKeyByIndex = async (req, res) => {
    let query = {
        index: req.query.index
    }
    try {
        let result = await Qcos.GetObjectNameByIndex(query)
        res.send(msg.genSuccessMsg('获取成功', result))
    } catch (error) {
        res.send(msg.genFailedMsg('获取失败', error))
    }
}