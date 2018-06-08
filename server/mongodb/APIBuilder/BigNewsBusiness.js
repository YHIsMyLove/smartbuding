const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const BigNews = mongoose.model('BigNewsModel')

var getBigNewsByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await BigNews.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateBigNews = async (req, res) => {

    if(!req.body.BgiNewsImg){
		return res.send(msg.genFailedMsg('err-> BgiNewsImg is not found'))
	}
	if(!req.body.BgiNewsTitle){
		return res.send(msg.genFailedMsg('err-> BgiNewsTitle is not found'))
	}
	if(!req.body.BgiNewsContent){
		return res.send(msg.genFailedMsg('err-> BgiNewsContent is not found'))
	}
	if(!req.body.ProjID){
		return res.send(msg.genFailedMsg('err-> ProjID is not found'))
	}
	

    try {
        if (req.body._id) {
            await BigNews.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _BigNews = new BigNews(req.body)
            await _BigNews.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listBigNews = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await BigNews.list(query);
        count = await BigNews.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delBigNewsByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await BigNews.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

exports.GetBigNewsByID = getBigNewsByID
exports.CreateOrUpdateBigNews = createOrUpdateBigNews
exports.ListBigNews = listBigNews
exports.DelBigNewsByID = delBigNewsByID