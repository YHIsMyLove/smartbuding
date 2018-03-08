const mongoose = require("mongoose")
const msg = require("../../utils/message")

const userSession = mongoose.model('UserSession')
const user = mongoose.model('User')
const area = mongoose.model('Area')
const proj = mongoose.model('Proj')

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
        res.send(msg.genSuccessMsg('登录成功'))
    } catch (error) {
        return res.send(msg.genFailedMsg('未知错误!'))
    }
}

//获取项目信息[err]
exports.getProjsbyUser = async (req, res) => {
    var query = {
        UserID: req.body.UserID
    }
    let _user = await user.findOne({ UserID: query.UserID }).exec()
    if (!_user) return res.send(msg.genFailedMsg('该人员不存在!'))
    let projs = _user.UserProjs
    let match_proj = Array.isArray(projs) ? { $in: projs } : projs;
    let _proj = await proj.find({ projid: match_proj }).exec()
    if (_proj.length == 0) return res.send(msg.genFailedMsg('该人员尚未分配项目!'))

    let result = _proj.map(i => {
        return {
            "UserID": _user.UserID,
            "UserName": _user.UserName,
            "ProjName": _proj.projname
        }
    })

    await res.send(msg.genSuccessMsg('登录成功', result))
}

//获取区域信息
exports.getAreas = async (req, res) => {
    let query = {}
    let _areas = await area.list()
    return req.send(msg.genSuccessMsg('获取区域信息成功', _areas))
}

//获取项目信息
exports.getProjbyArea = async (req, res) => {
    let match_proj = Array.isArray(req.query.projs) ? { $in: req.query.projs } : req.query.projs;
    let _projs = await proj.find({ areaid: match_proj }).exec()
    return req.send(msg.genSuccessMsg('获取区域信息成功', _projs))
}

//获取设备列表
exports.getDevs = async (req, res) => {

}