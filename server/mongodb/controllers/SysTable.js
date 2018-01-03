const mongoose = require('mongoose');
const SysField = mongoose.model('SysField');
const SysTable = mongoose.model('SysTable');
const { wrap: async } = require('co');
const msg = require('../../utils/message')

exports.getbyid = async(function* (req, res) {
    if (!id) {
        return res.send(msg.genFailedMsg('id不能为空'))
    }
    try {
        res.send(msg.genSuccessMsg('读取列表成功', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('获取列表失败', error))
    }
})
exports.list = async(function* (req, res) {
    try {
        if (!req.query.SysFieldID) {
            return res.send(msg.genFailedMsg('获取列表失败', '请输入ID'))
        }
        var query = {
            page: parseInt(req.query.page) - 1,
            limit: parseInt(req.query.limit),
            SysFieldID: req.query.SysFieldID
        }
        var list = yield SysTable.list(query);
        var list_fields = yield SysField.find(JSON.parse(`{ "SysTabName": "${req.query.SysFieldID}" }`)).exec()
        let resultList = []
        if (list_fields.length > 0) {
            let info = list_fields[0].SysFieldInfo
            let info_keys = Object.keys(JSON.parse(info))
            info_keys.forEach(i => {
                resultList.push(i)
            })
        } else {
            return res.send(msg.genFailedMsg('获取列表失败'))
        }
        list = list.map(i => {
            let tmpstr = `{"_id":"${i._id}","SysFieldID":"${i.SysFieldID}",`
            let index = 0
            resultList.forEach(i2 => {
                tmpstr += `"${i2}":"${i["item" + index]}",`
                index++
            })
            return JSON.parse(tmpstr.substr(0, tmpstr.length - 1) + "}")
        })
        var count = yield SysTable.count();
        res.send(msg.genSuccessMsg('读取列表成功', list, { count: count }))
    } catch (error) {
        console.log(error)
        res.send(msg.genFailedMsg('获取列表失败', error))
    }
})
exports.create = async(function* (req, res) {
    if (Object.keys(req.body).length === 0) {
        return res.send(msg.genFailedMsg('body不能为空'))
    }
    try {
        console.log(req.body)
        let sysTable = new SysTable(req.body);
        yield sysTable.updateAndSave();
        res.send(msg.genSuccessMsg('插入列表成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('插入列表失败', error))
    }
})
exports.update = async(function* (req, res) {
    try {
        res.send(msg.genSuccessMsg('读取列表成功', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('获取列表失败', error))
    }
})
exports.delete = async(function* (req, res) {
    try {
        res.send(msg.genSuccessMsg('读取列表成功', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('获取列表失败', error))
    }
})