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
const Meeting = mongoose.model('Meeting')
const MeetingMinutes = mongoose.model('MeetingMinutes')

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

    if (!ProjID) {
        return res.send(msg.genFailedMsg('请输入项目ID'))
    }

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
                UserCardID: i.UserCardID,
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

            return res.send(msg.genSuccessMsg("查询成功", users, { count: count }))
        } catch (error) {
            return res.send(msg.genFailedMsg("查询失败"))
        }
    }
    return res.send(msg.genFailedMsg("查询失败"))
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
            await UserDept.findOne(query).remove()
            await UserRole.findOne(query).remove()
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

        let data = result//.filter(i => i.item0 != 'Root')
            .map(i => {
                return {
                    label: i.item0,
                    value: { id: i._id, type: "dept" },
                    _id: i._id,
                    Name: i.item0,
                    children: [],
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
    console.log('获取省份资料')
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
                Name: i.item0,
                data: { id: i._id, type: "city" },
                _id: i._id
            }
        })
        console.log(result)
        res.send(msg.genSuccessMsg('获取成功', result))
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败"))
    }
}

/**
 * 根据项目获取城市和省份
 * @param {*} ProjID 
 */
var getProvAndCity_byProj = async (ProjID) => {
    return new Promise(async (res, rej) => {
        let cityid = await SysTable.findOne({ _id: ProjID })
        let cityinfo = await SysTable.findOne({ _id: cityid.item1 })
        let provinfo = await SysTable.findOne({ _id: cityinfo.item1 })
        try {
            let result = {
                City: {
                    _id: cityinfo._id,
                    Name: cityinfo.item0
                },
                Prov: {
                    _id: provinfo._id,
                    Name: provinfo.item0
                }
            }
            res(result)
        } catch (error) {
            rej(error)
        }
    })
}
//根据城市获取项目信息
exports.GetProjByCityID = async (req, res) => {

    let query = {}
    if (req.query.CityID != 'undefined') {
        query = {
            item1: req.query.CityID,
            SysFieldID: "proj"
        }
    } else {
        query = {
            SysFieldID: "proj"
        }
    }
    //if (query.item1 == undefined) return res.send(msg.genFailedMsg('请输入城市ID'))
    try {
        let projs = await SysTable.find(query)

        if (projs.length == 0) {
            return res.send(msg.genSuccessMsg('查询成功'))
        }

        var result2 = []
        projs.forEach(async i => {
            var prov_city = await getProvAndCity_byProj(i._id)
            result2.push({
                _id: i._id,
                Name: i.item0,
                label: i.item0,
                value: { id: i._id },
                City: prov_city.City,
                Prov: prov_city.Prov
            })
            if (result2.length == projs.length) {
                return res.send(msg.genSuccessMsg('查询成功', result2))
            }
        })
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败->" + error))
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
                let userdeptinfo = userdeptlist.find(d => d.UserID == i._id)
                //查询部门数据
                let r = {
                    _id: i._id,
                    UserID: i.UserID,
                    UserPwd: i.UserPwd,
                    UserName: i.UserName,
                    UserSex: i.UserSex,
                    UserAge: i.UserAge,
                    UserPhoneNum: i.UserPhoneNum,
                    UserInDept: userdeptinfo != undefined,
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

            let udept = await UserDept.find({ ProjID: ProjID, DeptID: DeptID })
            count = udept.length
            let q = result.map(i => {
                return {
                    "_id": i.UserID
                }
            })


            //查询用户数据
            let users = await User.find({ _id: { $in: q } })
                .sort({ createdAt: -1 })
                .exec()

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
        item1: req.query.ProvID,
    }
    if (!query.item1) return res.send(msg.genFailedMsg('请输入省份ID'))
    try {
        let citys = await SysTable.find(query).select('_id')
        let q = citys.map(i => i._id)
        let projs = await SysTable.find({ item1: { $in: q } })

        // let result = projs.map(i => {
        //     return {
        //         Name: i.item0,
        //         _id: i._id
        //     }
        // })
        // res.send(msg.genSuccessMsg('获取成功', result))

        var result2 = []
        projs.forEach(async i => {
            var prov_city = await getProvAndCity_byProj(i._id)
            result2.push({
                _id: i._id,
                Name: i.item0,
                City: prov_city.City,
                Prov: prov_city.Prov
            })
            if (result2.length == projs.length) {
                return res.send(msg.genSuccessMsg('查询成功', result2))
            }
        })


    } catch (error) {
        res.send(msg.genFailedMsg("获取失败"))
    }
}

/****************************************************************** */
/**用户角色管理*******************************************************/
/****************************************************************** */
//获取角色资料
exports.GetRole = async (req, res) => {
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
                desc: i.item1
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
    res.send(msg.genSuccessMsg('获取成功'))
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
exports.GetMeetings = async (req, res) => {
    let query = {
        ProjID: req.query.ProjID,
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    if (!query.ProjID) return res.send(msg.genFailedMsg('项目ID不允许为空'))
    try {
        let result = await Meeting.list(query)

        let data = result.map(i => {
            return {
                _id: i._id,
                MeetingName: i.MeetingName,
                MeetingCreatedAt: moment(i.MeetingCreatedAt).format('YYYY-MM-DD hh:mm:ss'),
                Compere: i.Compere
            }
        })
        let count = await Meeting.count()
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
exports.InsertOrUpdateMeeting = async (req, res) => {
    try {
        if (req.body._id == undefined) {
            let meeting = new Meeting(req.body)
            await meeting.updateAndSave();
        } else {
            let meeting = req.meeting
            meeting = Object.assign(Meeting, req.body);
            await meeting.updateAndSave();
        }
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('保存失败->' + error))
    }
}

//InsertOrUpdateMeeting
//插入或修改会议内容
exports.InsertOrUpdateMeetingContent = async (req, res) => {
    try {
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
}

//删除会议内容
exports.DelMeetingContent = async (req, res) => {

}

/**
 * 获取会议内容信息
 * @param {*} req 
 * @param {*} res 
 */
exports.GetMeetingContents_APP = async (req, res) => {
    let query = {
        MeetingID: req.query.MeetingID
    }
    try {
        let result = await MeetingMinutes.find(query)
        let deptids = await UserDept.find({ UserID: req.query.UserID }).select('DeptID')
        let depts = deptids.map(i => i.DeptID)
        console.log(depts)
        let data = result.map(i => {
            // console.log(i.Depts)
            // console.log(depts.indexOf(i.Depts[0].DeptID))
            // console.log(i.Depts.filter(d => { return depts.indexOf(d.DeptID) }))
            // console.log('*****************')
            return {
                _id: i._id,
                MeetingID: i.MeetingID,
                MeetingTitle: i.MeetingTitle,
                Content: i.Content,
                Depts: i.Depts.map(dept => {
                    return {
                        _id: dept.DeptID,
                        Name: dept.DeptName
                    }
                }),
                IsRelational: i.Depts.filter(d => depts.indexOf(d.DeptID) >= 0).length > 0
            }
        })
        res.send(msg.genSuccessMsg('获取成功', data))
    } catch (error) {
        res.send(msg.genFailedMsg('获取失败' + error))
    }
}

/**
 * 获取会议信息
 * @param {*} req 
 * @param {*} res 
 */
exports.GetMeetings_APP = async (req, res) => {
    let query = {
        ProjID: req.query.ProjID,
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    if (!query.ProjID) return res.send(msg.genFailedMsg('项目ID不允许为空'))
    try {
        let result = await Meeting.list(query)

        let deptids = await UserDept.find({ UserID: req.query.UserID }).select('DeptID')
        let depts = deptids.map(i => i.DeptID)
        console.log(depts)

        let meetingids = result.map(i => i._id)
        let result2 = await MeetingMinutes.find({ MeetingID: { $in: meetingids } }).select("Depts")

        let count2 = 0;
        result2.forEach(i => {
            if (i.Depts.filter(d => depts.indexOf(d.DeptID) >= 0).length > 0) {
                count2++
            }
        })

        let data = result.map(i => {
            return {
                _id: i._id,
                MeetingName: i.MeetingName,
                MeetingCreatedAt: moment(i.MeetingCreatedAt).format('YYYY-MM-DD hh:mm:ss'),
                Compere: i.Compere,
                RelationalCount: count2
            }
        })

        let count = await Meeting.count()
        res.send(msg.genSuccessMsg('查询成功', data, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('查询失败->' + error))
    }
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
                        res.send(msg.genSuccessMsg('上传成功', `http://${qcospath}`))
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
/****************************************************************** */
/**设备管理***********************************************************/
/****************************************************************** */
