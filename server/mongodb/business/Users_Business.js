const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const YS = require('../../utils/yingshi')
const Qcos = require('../../utils/Qcos.js')

const UserSession = mongoose.model('UserSession')
const SysTable = mongoose.model('SysTable')
const User = mongoose.model('User')
const UserDept = mongoose.model('UserDept')
const UserProj = mongoose.model('UserProj')
const UserRole = mongoose.model('UserRole')

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

/**
 * @api {POST} /api/Login?UserID
 * @apiName 登录
 * @apiGroup User
 * @apiParam {String} UserID 用户登录ID.
 * @apiParam {String} UserPwd 用户登录密码.
 * @apiError UserNotFound code:-1 用户没有找到
 * @apiError PassWorldErr code:-2 密码错误
 * @apiError NoAuth code:-3 没有权限
 * @apiError Other code:-4 未知错误
 * 
 * @apiSuccess {String} UserID 用户ID
 * @apiSuccess {String} SessionID 用户Session
 * @apiSuccess {String} YSToken 萤石的Token
 * 
 * @apiSuccessExample {json} Success-Response:
 * {
 *  "UserID":"xx"
 *  "SessionID":"xx"
 *  "YSToken":"xx"
 * }
 */
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
            //console.log('1-----' + session)
            if (session) sessionid = session._id

            //2. 匹配账号密码
            let _user = await User.findOne({ UserID: query.UserID }).exec()
            //console.log('2-----' + _user)
            if (!_user) return res.send(msg.genFailedMsg('该账号不存在!', { code: -1 }))
            if (_user.UserPwd != query.UserPwd) return res.send(msg.genFailedMsg('密码错误!', { code: -2 }))

            //2. 查询匹配项目 若没有则不允许登录
            let haveProj = await UserProj.findOne({ UserID: _user._id })
            //console.log('3-----')
            if (!haveProj) {
                return res.send(msg.genFailedMsg('没权限登录', { code: -3 }))
            }
            //3. 将数据存进/更新session表
            if (sessionid == '') {
                //console.log('4-----' + sessionid)
                let _usersession = new UserSession({
                    UserID: _user.UserID
                })
                await _usersession.updateAndSave()
                sessionid = _usersession._id
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
            return res.send(msg.genFailedMsg('未知错误!' + error, { code: -4 }))
        }
    }
}


// /**
//  * @api {POST} /api/SetUserHeadImage
//  * @apiName 设置用户头像
//  * @apiGroup User
//  * @apiParam {File} file 用户上传的文件.
//  * @apiParam {String} Type 用户上传的文件类型,默认用户头像.
//  * 
//  * @apiError FilleNotFound code:-1 文件没有找到
//  * @apiError Other code:-2 未知错误
//  * @apiError ReNameFileErr code:-3 更新文件名称失败
//  * @apiError ReNameFileErr code:-4 压缩图片大小失败
//  * @apiError UpLoadErr code:-5 上传图片失败
//  * 
//  * @apiSuccess {String} Path 云端图片路径
//  * 
//  * @apiSuccessExample {json} Success-Response:
//  * {
//  *  "Path":"https://xxxxxxxx.com/xxx.jpg"
//  * }
//  */
// exports.SetUserImage = async (req, res) => {

//     let query = {
//         uploadType: req.Type || "UserHeadImage",
//     }

//     var form = new multiparty.Form({ uploadDir: './static' });
//     form.parse(req, async (err, fields, files) => {
//         if (!files.file && files.file.length > 0) {
//             return res.send(msg.genFailedMsg('上传失败', { code: -1, msg: '请输入文件' }))
//         }
//         if (err) {
//             return res.send(msg.genFailedMsg('上传失败', { code: -2, msg: err }))
//         }
//         var inputFile = files.file[0];
//         var uploadedPath = inputFile.path;
//         var dstPath = './static/' + inputFile.originalFilename;

//         let rename_err = await renameFile(uploadedPath, dstPath)
//         if (rename_err) {
//             return res.send(msg.genFailedMsg('上传失败', { code: -3, msg: rename_err }))
//         }
//         else {
//             files.file.path = dstPath;
//             let data = files;
//             let resizeimg_result = await ImageMagick.ResizeImg(dstPath)
//             if (resizeimg_result.err) {
//                 fs.unlinkSync(resizeimg_result.data)
//                 fs.unlinkSync(files.file.path)
//                 return res.send(msg.genFailedMsg('上传失败', { code: -4, msg: resizeimg_result.err }))
//             } else {
//                 let upload_result = await Qcos.uploadPromise(resizeimg_result.data)
//                 if (upload_result.err) {
//                     fs.unlinkSync(resizeimg_result.data)
//                     fs.unlinkSync(files.file.path)
//                     return res.send(msg.genFailedMsg('上传失败', { code: -5, msg: upload_result.err }))
//                 } else {
//                     fs.unlinkSync(resizeimg_result.data)
//                     fs.unlinkSync(files.file.path)
//                     res.send(genSuccessMsg('上传成功', upload_result))
//                 }
//             }
//         }
//     })
// }

// /**
//  * 更新文件名
//  * @param {*} oldname 旧的地址
//  * @param {*} newname 新的地址
//  */
// let renameFile = async (oldname, newname) => {
//     return new Promise((res, rej) => {
//         require('fs').rename(oldname, newname, (err) => {
//             if (err) rej(err)
//             res()
//         })
//     })
// }


/**
 * POST 检查Session
 * @param {*} req {SessionID}
 * @param {*} res 
 */
exports.checkSession = async (req, res) => {
    let query = {
        _id: req.body.SessionID
    }
    if (!_id) return res.send(msg.genFailedMsg('请输入SessionID'))
    let session = await UserSession.findOne(query).exec()
    if (session) {
        return res.send(msg.genSuccessMsg('登录成功'))
    }
    res.send(msg.genFailedMsg('无效session,请登录'))
}

/**
 * POST 退出登录
 * @param {*} req {SessionID}
 * @param {*} res 
 */
exports.LogOut = async (req, res) => {
    let query = {
        _id: req.body.SessionID
    }
    if (!_id) return res.send(msg.genFailedMsg('请输入SessionID'))
    try {
        await UserSession.findOne(query).remove()
        res.send(msg.genSuccessMsg('注销成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('注销失败', error))
    }
}

/**
 * GET 获取用户信息 Get for Login over
 * @param {*} req {SessionID}
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
 * GET 获取用户相关的省份
 * @param {*} req {UserID}
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
 * GET 获取用户相关的项目
 * @param {*} req {UserID}
 * @param {*} res 
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

    //组装数据
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
}