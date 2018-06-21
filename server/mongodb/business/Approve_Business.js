const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const Approve = mongoose.model('Approve')

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
    let query = {}
    Object.keys(req.query).forEach(item => {
        //console.log(req.query[item])
        query[item] = req.query[item]
    })
    let result = await Approve.find(query)
    res.send(msg.genSuccessMsg('查询成功', result))
}

/**
 * 审批状态介绍 
 * -1 拒绝 
 * -2 撤销
 *  0 待提交
 *  1 待审批
 *  2 完成
 */
var optionApprove = async (req, res) => {
    var query = {
        ApproveID: req.body.ApproveID,
        UserID: req.body.UserID,
        option: req.body.option
    }
    //let result = await Approve.find({ _id: query.ApproveID })
    await Approve.updateOne({ _id: query.ApproveID }, { approveState: query.option })
    // switch (result.approveState) {
    //     case -1:
    //         res.send(msg.genFailedMsg('该审批已拒绝'))
    //         break
    //     case -2:
    //         res.send(msg.genFailedMsg('该审批已撤销'))
    //         break;
    //     case 2:
    //         res.send(msg.genFailedMsg('该审批已完成'))
    //         break;
    //     case 0:
    //     case 1:
    //         result = await Approve.updateOne({ _id: query.ApproveID }, { approveState: query.option })
    //         break
    // }
    res.send(msg.genSuccessMsg('操作完成'))
}

// var agreeApprove = async (Req, res) => {
//     var query = {
//         ApproveID: req.body.ApproveID,
//         UserID: req.body.UserID,
//     }
//     await Approve.find({ _id: query.ApproveID }).update({ approveState: -1 })
//     res.send(msg.genSuccessMsg('拒绝完成'))
// }

exports.ListApprove = listApprove
exports.OptionApprove = optionApprove
exports.CreateApprove = createApprove






















