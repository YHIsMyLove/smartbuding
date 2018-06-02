const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const Devices = mongoose.model('DevicesModel')

var getDevicesByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await Devices.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateDevices = async (req, res) => {

    if(!req.body.DevID){
		return res.send(msg.genFailedMsg('err-> DevID is not found'))
	}
	if(!req.body.DevName){
		return res.send(msg.genFailedMsg('err-> DevName is not found'))
	}
	if(!req.body.ProjID){
		return res.send(msg.genFailedMsg('err-> ProjID is not found'))
	}
	

    try {
        if (req.body._id) {
            await Devices.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _Devices = new Devices(req.body)
            await _Devices.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listDevices = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await Devices.list(query);
        count = await Devices.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delDevicesByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await Devices.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

exports.GetDevicesByID = getDevicesByID
exports.CreateOrUpdateDevices = createOrUpdateDevices
exports.ListDevices = listDevices
exports.DelDevicesByID = delDevicesByID