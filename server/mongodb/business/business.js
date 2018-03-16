const mongoose = require("mongoose")
const msg = require("../../utils/message")

const userSession = mongoose.model('UserSession')
const SysTable = mongoose.model('SysTable')
const User = mongoose.model('User')
const UserDept = mongoose.model('UserDept')
const UserProj = mongoose.model('UserProj')

//登录
exports.login = async (req, res) => {
    var query = {
        UserID: req.body.UserID,
        UserPwd: req.body.UserPwd
    }
    try {
        //1. 查询session
        let session = await userSession.find({ UserID: query.UserID }).exec()
        if (session.length == 0) {
            //2. 匹配账号密码
            let _user = await User.find({ UserID: query.UserID }).exec()
            if (_user.length == 0) return res.send(msg.genFailedMsg('该账号不存在!'))
            if (_user.UserPwd != query.UserPwd) return res.send(msg.genFailedMsg('密码错误!'))
        }
        //3. 将数据存进session表
        //4. 取出萤石的session
        res.send(msg.genSuccessMsg('登录成功', _user))
    } catch (error) {
        return res.send(msg.genFailedMsg('未知错误!'))
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

    console.log(DeptID)

    if (isEdit == 'true' && DeptID != -1) {// 
        console.log('编辑模式且有部门ID')
        let list = await User.find({})
            .sort({ createdAt: -1 })
            .limit(query.limit)
            .skip(query.limit * query.page)
            .exec();
        let ids = list.map(i => i._id.toString())
        let userdeptlist = await UserDept.find({ UserID: { $in: ids }, DeptID: DeptID, ProjID: ProjID }).exec()//
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
                UserInDept: userprojlist.find(d => d.UserID == i._id) != undefined
            }
            return r
        })
        count = await User.count()
        return res.send(msg.genSuccessMsg("查询成功", data, { count: count }))
    }

    if (isEdit == 'true' && DeptID == -1) {
        console.log('编辑模式部门ID=-1')
        let list = await User.find({})
            .sort({ createdAt: -1 })
            .limit(query.limit)
            .skip(query.limit * query.page)
            .exec();
        count = await User.count()
        return res.send(msg.genSuccessMsg("查询成功", list, { count: count }))
    }

    //
    console.log(isEdit + '********************************')
    if (isEdit == 'false' && DeptID != -1) {
        console.log('非编辑模式且有部门ID')
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
        item1: req.query.item1,
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

//根据省市获取项目信息
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