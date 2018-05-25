const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const User = mongoose.model('User');
const Device = mongoose.model('Device')


/**
 * @api {POST} /api/tp/checkID
 * @apiName 检查ID是否符合规定
 * @apiGroup third-party
 * @apiParam {String} UserID 用户ID.
 * 
 * @apiSuccess {String} isOK 是否符合规定 0:符合 1:不符合
 */
var checkID = async (req, res) => {
    if (!req.query.UserID) {
        return res.send(msg.genFailedMsg('id不能为空'))
    }
    let query = {
        UserID: req.query.UserID
    }
    let result = await User.findOne(query)
    if (!result) {
        return res.send(msg.genSuccessMsg('允许创建'))
    }
    res.send(msg.genFailedMsg('UserID重复!'))
}

/**
 * @api {POST} /api/tp/saveOrUpdateUser
 * @apiName 插入/修改用户
 * @apiGroup third-party
 * @apiParam {String} isCreate 标识新增Or修改. 1:新增 0:修改
 * @apiParam {String} UserID 用户登录ID.
 * @apiParam {String} UserName 用户姓名.
 * @apiParam {String} UserAge 用户年龄.
 * @apiParam {String} UserSex 用户性别.
 * @apiParam {String} UserPhoneNum 用户电话号码.
 * @apiParam {String} UserEmail 用户邮箱.
 * @apiParam {String} UserCardID 用户身份证.
 * 
 * @apiError isCreateIsNull code:-1 isCreate标识为空
 * @apiError UserIDOrUserNameIsNull code:-2 ID或姓名为空
 * @apiError UserIDAlreadyExisted code:-3 ID不允许重复
 * @apiError NoAuth code:-3 没有权限
 * @apiError Other code:-4 未知错误
 */
var saveOrUpdateUser = async (req, res) => {
    try {
        var isCreate = req.body.isCreate
        if (!isCreate) return res.send(msg.genFailedMsg('插入失败!isCreate不允许为空', { code: -1 }))
        var _user = {
            UserID: req.body.UserID,
            UserName: req.body.UserName,
        }
        if (!_user.UserID || !_user.UserName) return res.send(msg.genFailedMsg('插入失败!用户ID或用户姓名为空', { code: -2 }))
        let result = await User.findOne({ UserID: _user.UserID })
        if (result) return res.send(msg.genFailedMsg('插入失败!用户ID重复', { code: -3 }))

        if (isCreate === '1') {
            let user = new User(req.body)
            await user.updateAndSave();
            res.send(msg.genSuccessMsg('插入成功', user))
        } else {
            await User.updateOne({ UserID: _user.UserID }, req.body).exec()
            res.send(msg.genSuccessMsg('修改成功'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('未知错误:' + error))
    }
}

/**
 * @api {POST} /api/SetDoorIOInfo
 * @apiName 插入门禁进出记录
 * @apiGroup third-party
 * @apiParam {String} UserID 用户登录ID.
 * @apiParam {String} DoorID 门禁ID.
 * @apiParam {String} IOTime 进出时间.
 * @apiParam {String} IO 进/出.
 * @apiError UserNotFound code:-1 用户没有找到
 * @apiError DoorNotFound code:-2 门禁设备没找到
 * @apiError NoAuth code:-3 没有权限
 * @apiError Other code:-4 未知错误
 */
var saveDoorIO = async (req, res) => {
    var query = {
        UserID: req.body.UserID,
        DoorID: req.body.DoorID,
        IOTime: req.body.IOTime,
        IO: req.body.IO
    }
    //1. 判断权限
    if (!query.UserID) return res.send(msg.genFailedMsg('用户没有找到', {
        code: -1
    }))
    if (!query.DoorID) return res.send(msg.genFailedMsg('门禁设备没找到', {
        code: -2
    }))
    try {
        let door = new DoorIOInfo(query);
        await door.updateAndSave();
        return res.send(msg.genSuccessMsg('插入成功'))
    } catch (error) {
        code = -4
        return res.send(msg.genFailedMsg('未知错误:' + error, {
            code: -4
        }))
    }
}

/**
 * 
 */
var saveOrUpdateDev = async (req, res) => {
    if (req.body._id) {
        let dev = new Device(req.body)
        await dev.updateAndSave()
        res.send(msg.genSuccessMsg('update'))
    } else {
        let dev = req.body
        dev = Object.assign(Device, req.body);
        await dev.updateAndSave()
        res.send(msg.genSuccessMsg('save'))
    }
}

exports.SAVEORUPDATEUSER = saveOrUpdateUser
exports.SAVEORUPDATEDEV = saveOrUpdateDev
exports.SAVEDOORIO = saveDoorIO
exports.CHECKID = checkID
