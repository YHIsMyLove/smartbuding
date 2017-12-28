const mongoose = require('mongoose');
const CaseShow = mongoose.model('CaseShow');
const { wrap: async } = require('co');
const msg = require('../../utils/message')

/**
 * 分页获取案例展示列表
 */
exports.GetCase = async(function* (req, res) {
    try {
        var query = {
            page: parseInt(req.query.page) - 1,
            limit: parseInt(req.query.limit)
        }
        var list = yield CaseShow.list(query);
        var count = yield CaseShow.count();
        res.send(msg.genSuccessMsg('读取案例展示列表成功', list, { count: count }))
    } catch (error) {
        console.log(error);
        res.send(msg.genFailedMsg('获取案例展示列表失败'))
    }
})

exports.GetCaseByUserID = function (req, res) {
    res.send('GetCaseByUserID' + req.query.UserID)
}
exports.SaveCase = function (req, res) {
    res.send('SaveCase')
}
exports.DelCaseByID = function (req, res) {
    res.send('DelCaseByID' + req.query.UserID)
}