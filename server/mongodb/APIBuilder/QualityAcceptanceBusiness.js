const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const QualityAcceptance = mongoose.model('QualityAcceptanceModel')

var getQualityAcceptanceByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await QualityAcceptance.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateQualityAcceptance = async (req, res) => {

    if(!req.body.QAImg){
		return res.send(msg.genFailedMsg('err-> QAImg is not found'))
	}
	if(!req.body.QARecipient){
		return res.send(msg.genFailedMsg('err-> QARecipient is not found'))
	}
	if(!req.body.QATitle){
		return res.send(msg.genFailedMsg('err-> QATitle is not found'))
	}
	if(!req.body.QAContent){
		return res.send(msg.genFailedMsg('err-> QAContent is not found'))
	}
	if(!req.body.QATime){
		return res.send(msg.genFailedMsg('err-> QATime is not found'))
	}
	if(!req.body.QAState){
		return res.send(msg.genFailedMsg('err-> QAState is not found'))
	}
	

    try {
        if (req.body._id) {
            await QualityAcceptance.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _QualityAcceptance = new QualityAcceptance(req.body)
            await _QualityAcceptance.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listQualityAcceptance = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await QualityAcceptance.list(query);
        count = await QualityAcceptance.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delQualityAcceptanceByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await QualityAcceptance.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

exports.GetQualityAcceptanceByID = getQualityAcceptanceByID
exports.CreateOrUpdateQualityAcceptance = createOrUpdateQualityAcceptance
exports.ListQualityAcceptance = listQualityAcceptance
exports.DelQualityAcceptanceByID = delQualityAcceptanceByID