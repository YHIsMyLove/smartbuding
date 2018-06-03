const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const MenuAuth = mongoose.model('MenuAuthModel')

var getMenuAuthByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await MenuAuth.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateMenuAuth = async (req, res) => {

    if(!req.body.MenuID){
		return res.send(msg.genFailedMsg('err-> MenuID is not found'))
	}
	if(!req.body.RoleID){
		return res.send(msg.genFailedMsg('err-> RoleID is not found'))
	}
	

    try {
        if (req.body._id) {
            await MenuAuth.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _MenuAuth = new MenuAuth(req.body)
            await _MenuAuth.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listMenuAuth = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await MenuAuth.list(query);
        count = await MenuAuth.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delMenuAuthByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await MenuAuth.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

exports.GetMenuAuthByID = getMenuAuthByID
exports.CreateOrUpdateMenuAuth = createOrUpdateMenuAuth
exports.ListMenuAuth = listMenuAuth
exports.DelMenuAuthByID = delMenuAuthByID