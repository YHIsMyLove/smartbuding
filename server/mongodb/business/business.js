const mongoose = require("mongoose")
const msg = require("../../utils/message")

const userSession = mongoose.model('UserSession')
const SysTable = mongoose.model('SysTable')
const user = mongoose.model('User')
const UserDept = mongoose.model('UserDept')

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
            let _user = await user.find({ UserID: query.UserID }).exec()
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

//根据项目ID获取部门数据
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

//根据项目ID获取用户数据
exports.GetUsersByUserDept_ProjID = async (req, res) => {
    let query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit),
        ProjID: req.query.ProjID
    }

    //1. 查询总数
    let count = await UserDept.find({ ProjID: query.ProjID }).count()
    //2. 查询IDs
    let users = await UserDept.find(query).select("UserID")
        .populate({
            path: 'User',
            select: "UserName UserSex UserAge UserPhoneNum"
        })
    //3. 查询人员信息
    let users = await user.find({ UserID: { $in: ids } })
}

//设置用户部门表
exports.SetUser2DeptByUserID = async (req, res) => {
    let query = {}

}
