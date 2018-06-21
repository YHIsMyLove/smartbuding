const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const Approve = mongoose.model('Approve')

/**
 * 审批状态介绍 
 * -1 拒绝 
 * -2 撤销
 *  0 待提交
 *  1 待审批
 *  2 完成
 */
var createApprove = async (req, res) => {
    try {
        var _Approve = new Approve(req.body)
        await _Approve.updateAndSave()
        res.send(msg.genSuccessMsg('create success'))
    } catch (error) {
        res.send(msg.genFailedMsg('create error'))
    }
}

var listApprove = async (req, res) => {
    let result = await Approve.find()
    res.send(msg.genSuccessMsg('查询成功', result))
}

var optionApprove = async (req, res) => {
    var query = {
        ApproveID: req.body.ApproveID,
        UserID: req.body.UserID,
        option: req.body.option
    }
    let result = await Approve.find({ _id: query.ApproveID })
    switch (result.approveState) {
        case -1:
            break;

        default:
            break;
    }
    //.update({ approveState: query.option })
    res.send(msg.genSuccessMsg('拒绝完成'))
}

var agreeApprove = async (Req, res) => {
    var query = {
        ApproveID: req.body.ApproveID,
        UserID: req.body.UserID,
    }
    await Approve.find({ _id: query.ApproveID }).update({ approveState: -1 })
    res.send(msg.genSuccessMsg('拒绝完成'))
}

