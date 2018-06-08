const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const DoorIO = mongoose.model('DoorIOModel')

var getDoorIOByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await DoorIO.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateDoorIO = async (req, res) => {

    if(!req.body.IOTime){
		return res.send(msg.genFailedMsg('err-> IOTime is not found'))
	}
	if(!req.body.IOType){
		return res.send(msg.genFailedMsg('err-> IOType is not found'))
	}
	if(!req.body.UserID){
		return res.send(msg.genFailedMsg('err-> UserID is not found'))
	}
	if(!req.body.ProjID){
		return res.send(msg.genFailedMsg('err-> ProjID is not found'))
	}
	

    try {
        if (req.body._id) {
            await DoorIO.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _DoorIO = new DoorIO(req.body)
            await _DoorIO.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listDoorIO = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await DoorIO.list(query);
        count = await DoorIO.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delDoorIOByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await DoorIO.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

exports.GetDoorIOByID = getDoorIOByID
exports.CreateOrUpdateDoorIO = createOrUpdateDoorIO
exports.ListDoorIO = listDoorIO
exports.DelDoorIOByID = delDoorIOByID