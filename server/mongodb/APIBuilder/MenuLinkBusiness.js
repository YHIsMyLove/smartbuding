const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const MenuLink = mongoose.model('MenuLinkModel')

var getMenuLinkByID = async (req, res) => {
    var query = {
        _id: req.query._id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await MenuLink.findOne(query)
        return res.send(msg.genSuccessMsg('success', result))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

var createOrUpdateMenuLink = async (req, res) => {

    if(!req.body.MenuIcon){
		return res.send(msg.genFailedMsg('err-> MenuIcon is not found'))
	}
	if(!req.body.MenuName){
		return res.send(msg.genFailedMsg('err-> MenuName is not found'))
	}
	if(!req.body.MenuPath){
		return res.send(msg.genFailedMsg('err-> MenuPath is not found'))
	}
	

    try {
        if (req.body._id) {
            await MenuLink.updateOne({ _id: req.body._id }, req.body).exec()
            res.send(msg.genSuccessMsg('update success'))
        } else {
            var _MenuLink = new MenuLink(req.body)
            await _MenuLink.updateAndSave()
            res.send(msg.genSuccessMsg('create success'))
        }
    } catch (error) {
        res.send(msg.genFailedMsg('err->' + error))
    }
}

var listMenuLink = async (req, res) => {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    try {
        var list = await MenuLink.list(query);
        count = await MenuLink.count();
        res.send(msg.genSuccessMsg('list success', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('list err->' + error))
    }
}

var delMenuLinkByID = async (req,res)=>{
    var query = {
        _id: req.query.id
    }
    if (!query._id) return res.send(msg.genFailedMsg('err-> _id is not found'))
    try {
        var result = await MenuLink.findOne(query).remove()
        return res.send(msg.genSuccessMsg('success'))
    } catch (error) {
        return res.send(msg.genFailedMsg('err->' + error))
    }
}

exports.GetMenuLinkByID = getMenuLinkByID
exports.CreateOrUpdateMenuLink = createOrUpdateMenuLink
exports.ListMenuLink = listMenuLink
exports.DelMenuLinkByID = delMenuLinkByID