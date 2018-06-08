const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const Mechanical = mongoose.model('MechanicalModel')

var getMechanicalByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await Mechanical.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateMechanical = async (req, res) => {

    if(!req.body.mechanicalImg){
		return res.send(msg.genFailedMsg('err-> mechanicalImg is not found'))
	}
	if(!req.body.mechanicalName){
		return res.send(msg.genFailedMsg('err-> mechanicalName is not found'))
	}
	if(!req.body.mechanicalState){
		return res.send(msg.genFailedMsg('err-> mechanicalState is not found'))
	}
	if(!req.body.mechanicalUser){
		return res.send(msg.genFailedMsg('err-> mechanicalUser is not found'))
	}
	if(!req.body.mechanicalUserPhone){
		return res.send(msg.genFailedMsg('err-> mechanicalUserPhone is not found'))
	}
	

    try {
        if (req.body._id) {
            await Mechanical.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _Mechanical = new Mechanical(req.body)
            await _Mechanical.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listMechanical = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await Mechanical.list(query);
        count = await Mechanical.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delMechanicalByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await Mechanical.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

exports.GetMechanicalByID = getMechanicalByID
exports.CreateOrUpdateMechanical = createOrUpdateMechanical
exports.ListMechanical = listMechanical
exports.DelMechanicalByID = delMechanicalByID