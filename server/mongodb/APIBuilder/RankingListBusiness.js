const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const RankingList = mongoose.model('RankingListModel')

var getRankingListByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await RankingList.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateRankingList = async (req, res) => {

    if(!req.body.UserID){
		return res.send(msg.genFailedMsg('err-> UserID is not found'))
	}
	if(!req.body.RankingListTitle){
		return res.send(msg.genFailedMsg('err-> RankingListTitle is not found'))
	}
	if(!req.body.ProjID){
		return res.send(msg.genFailedMsg('err-> ProjID is not found'))
	}
	

    try {
        if (req.body._id) {
            await RankingList.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _RankingList = new RankingList(req.body)
            await _RankingList.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listRankingList = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await RankingList.list(query);
        count = await RankingList.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delRankingListByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await RankingList.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}




exports.GetRankingListByID = getRankingListByID
exports.CreateOrUpdateRankingList = createOrUpdateRankingList
exports.ListRankingList = listRankingList
exports.DelRankingListByID = delRankingListByID