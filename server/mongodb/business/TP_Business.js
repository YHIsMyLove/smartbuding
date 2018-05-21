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


var saveOrUpdateUser = async (req, res) => {
    res.send(msg.genSuccessMsg('test'))
}

var saveOrUpdateDev = async (req, res) => {
    res.send(msg.genSuccessMsg('test'))
}

var saveDoorIO = async (req, res) => {
    res.send(msg.genSuccessMsg('test'))
}

exports.SAVEORUPDATEUSER = saveOrUpdateUser
exports.SAVEORUPDATEDEV = saveOrUpdateDev
exports.SAVEDOORIO = saveDoorIO