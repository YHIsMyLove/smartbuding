const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const Approve = mongoose.model('Approve')
const ApproveLog = mongoose.model('ApproveLog')
const moment = require('moment')

var createApprove = async (req, res) => {
    try {
        var _Approve = new Approve(req.body)
        await _Approve.updateAndSave()

        var _ApproveLog = new ApproveLog({
            _approveID: _Approve._id,
            proposer: { id: _Approve.proposer, time: Date.now(), content: '', status: 0 },
            approver: { id: _Approve.approver, time: Date.now(), content: '', status: 0 },
            overseer: { id: _Approve.overseer, time: Date.now(), content: '', status: 0 },
        })
        await _ApproveLog.updateAndSave()

        res.send(msg.genSuccessMsg('create success'))
    } catch (error) {
        res.send(msg.genFailedMsg('create error->' + error))
    }
}

var listApprove = async (req, res) => {
    try {
        // Object.keys(req.query).forEach(item => {
        //     query[item] = req.query[item]
        // })
        let result = await ApproveLog.aggregate([
            { $match: { ProjID: req.query.ProjID } },
            { $lookup: { from: 'approves', localField: '_approveID', foreignField: '_id', as: 'approves' } },
            { $lookup: { from: 'users', localField: 'proposer.id', foreignField: 'UserID', as: 'proposerInfo' } },
            { $lookup: { from: 'users', localField: 'approver.id', foreignField: 'UserID', as: 'approverInfo' } },
            { $lookup: { from: 'users', localField: 'overseer.id', foreignField: 'UserID', as: 'overseerInfo' } },
        ])
        result = result.map(item => {
            return {
                proposer: {
                    _id: item.proposerInfo[0]._id,
                    UserName: item.proposerInfo[0].UserName,
                    status: item.proposer.status,
                    content: item.proposer.content,
                    time: moment(item.proposer.time).format('YYYY-MM-DD hh:mm')
                },
                approver: {
                    _id: item.approverInfo[0]._id,
                    UserName: item.approverInfo[0].UserName,
                    status: item.approver.status,
                    content: item.approver.content,
                    time: moment(item.approver.time).format('YYYY-MM-DD hh:mm')
                },
                overseer: {
                    _id: item.overseerInfo[0]._id,
                    UserName: item.overseerInfo[0].UserName,
                    status: item.overseer.status,
                    content: item.overseer.content,
                    time: moment(item.overseer.time).format('YYYY-MM-DD hh:mm')
                },
                approveContent: item.approves[0].approveContent,
                startTime: moment(item.approves[0].startTime).format('YYYY-MM-DD hh:mm'),
                _id: item.approves[0]._id
            }
        })
        res.send(msg.genSuccessMsg('查询成功', result))
    } catch (error) {
        res.send(msg.genFailedMsg('查询失败->' + error))
    }
}

/**
 * 审批状态介绍 
 * -1 拒绝 
 * -2 撤销
 *  0 待审批
 *  1 完成
 */
var optionApprove = async (req, res) => {
    try {
        var UserKind = req.body.UserKind
        var ApproveID = req.body.ApproveID
        var query = UserKind === 'proposer'
            ? {
                _approveID: ApproveID,
                'proposer.id': req.body.UserID
            }
            : UserKind == 'approver'
                ? {
                    _approveID: ApproveID,
                    'approver.id': req.body.UserID
                }
                : {
                    _approveID: ApproveID,
                    'overseer.id': req.body.UserID
                }
        var update = UserKind === 'proposer'
            ? {
                'proposer.status': req.body.status,
                'proposer.content': req.body.content,
                'proposer.time': Date.now()
            }
            : UserKind == 'approver'
                ? {
                    'approver.status': req.body.status,
                    'approver.content': req.body.content,
                    'approver.time': Date.now()
                }
                : {
                    'overseer.status': req.body.status,
                    'overseer.content': req.body.content,
                    'overseer.time': Date.now()
                }
        await ApproveLog.updateOne(query, update)
        let result = await ApproveLog.findOne(query)
        res.send(msg.genSuccessMsg('操作完成', result))
    } catch (error) {
        res.send(msg.genFailedMsg('error->' + error))
    }

}

exports.ListApprove = listApprove
exports.OptionApprove = optionApprove
exports.CreateApprove = createApprove






















