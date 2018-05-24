const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const User = mongoose.model('User');
const Device = mongoose.model('Device')


var saveOrUpdateUser = async (req, res) => {
    if (req.body._id) {
        let user = new User(req.body)
        await user.updateAndSave()
        res.send(msg.genSuccessMsg('update'))
    } else {
        let user = req.body.user
        user = Object.assign(User, req.body);
        await user.updateAndSave()
        res.send(msg.genSuccessMsg('save'))
    }
}

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

exports.SAVEORUPDATEUSER = saveOrUpdateUser
exports.SAVEORUPDATEDEV = saveOrUpdateDev
exports.SAVEDOORIO = saveDoorIO
