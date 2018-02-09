const mongoose = require('mongoose');
const Device = mongoose.model('Device');
const { wrap: async } = require('co');
const msg = require('../../utils/message')
const SystemConfig = require('../../config/config')
const Mock = require('mockjs')

//分页获取设备数据
exports.list = async(function* (req, res) {
    var query = {
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    var count = query.limit
    if (SystemConfig.MockData == 1) {
        var Random = Mock.Random
        var data = []
        while (count > 0) {
            var item = Mock.mock({
                'UserID|+1': 1,
                'DevName|1': ['摄像头', '门禁'],
                'DevClass|1': ['camera', 'door'],
                'DevDesc': Random.cparagraph(2, 3),
                'DevStatus': Random.integer(0, 1),
                'DevIp': Random.ip(),
                'DevPort': Random.integer(10000, 65535),
            })
            data.push(item)
            count--
        }
        res.send(msg.genSuccessMsg('读取设备列表成功', data, { count: 2000 }))
    }
    try {
        var list = yield Device.list(query);
        count = yield Device.count();
        res.send(msg.genSuccessMsg('读取设备列表成功', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('获取设备列表失败'))
    }
})