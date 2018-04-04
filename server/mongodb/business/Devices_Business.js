const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const YS = require('../../utils/yingshi')

const UserSession = mongoose.model('UserSession')
const SysTable = mongoose.model('SysTable')
const User = mongoose.model('User')
const UserDept = mongoose.model('UserDept')
const UserProj = mongoose.model('UserProj')
const UserRole = mongoose.model('UserRole')
const Device = mongoose.model('Device')

/**
 * GET 根据项目获取系统设备表
 * @param {*} req 
 * @param {*} res 
 */
exports.GetSysDeviceByProj = async (req, res) => {
    let query = {}
    res.send(msg.genSuccessMsg('获取成功'))
}

/**
 * POST 设置设备是否属于系统库
 * @param {*} req 
 * @param {*} res 
 */
exports.SetDevice = async (req, res) => {
    let query = {}
    res.send(msg.genSuccessMsg('设置成功'))
}