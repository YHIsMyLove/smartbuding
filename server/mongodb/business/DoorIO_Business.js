const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const moment = require('moment')
const DoorIOInfo = mongoose.model('DoorIOInfo')

/**
 * @api {POST} /api/SetDoorIOInfo
 * @apiName 插入门禁进出记录
 * @apiGroup DoorIO
 * @apiParam {String} UserID 用户登录ID.
 * @apiParam {String} DoorID 门禁ID.
 * @apiParam {String} I/OTime 进出时间.
 * @apiParam {String} I/O 进/出.
 * @apiError UserNotFound code:-1 用户没有找到
 * @apiError DoorNotFound code:-2 门禁设备没找到
 * @apiError NoAuth code:-3 没有权限
 * @apiError Other code:-4 未知错误
 */
exports.SetDoorIOInfo = async (req, res) => {
    var query = {
        UserID: req.body.UserID,
        DoorID: req.body.DoorID,
        IOTime: req.body.IOTime,
        IO: req.body.IO
    }
    //1. 判断权限
    if (!query.UserID) return res.send(msg.genFailedMsg('用户没有找到', { code: -1 }))
    if (!query.DoorID) return res.send(msg.genFailedMsg('门禁设备没找到', { code: -2 }))
    try {
        let door = new DoorIOInfo(query);
        await door.updateAndSave();
        return res.send(msg.genSuccessMsg('插入成功'))
    } catch (error) {
        code = -4
        return res.send(msg.genFailedMsg('未知错误:' + error, { code: -4 }))
    }
}