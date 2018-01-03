const mongoose = require('mongoose');
const SysField = mongoose.model('SysField');
const { wrap: async } = require('co');
const msg = require('../../utils/message')


exports.list = async(function* (req, res) {
    try {
        var query = {
            page: parseInt(req.query.page) - 1,
            limit: parseInt(req.query.limit)
        }
        var list = yield SysField.list(query);
        var count = yield SysField.count();
        res.send(msg.genSuccessMsg('读取字段管理列表成功', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('获取字段管理列表失败', error))
    }
})
//根据ID获取Content信息
exports.getbyid = async(function* (req, res) {
    try {
        var id = req.body.id
        console.log(id)
        if (!id) {
            res.send(msg.genFailedMsg('请输入id'))
        }
        var SysFieldInfo = yield SysField.load(id)
        res.send(msg.genSuccessMsg('获取字段详细信息成功', SysFieldInfo))
    } catch (error) {
        res.send(msg.genFailedMsg('获取字段详细信息失败', error))
    }
})
//保存字段信息
exports.create = async(function* (req, res) {
    if (Object.keys(req.body).length === 0) {
        return res.send(msg.genFailedMsg('body不能为空'))
    }
    if (!req.body.SysTabName ||
        !req.body.SysFieldInfo) {
        return res.send(msg.genFailedMsg('字段不能为空'))
    }
    let sysfield = new SysField(req.body);
    try {
        yield sysfield.updateAndSave();
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('保存失败', error))
    }
})
//删除字段信息
exports.delete = async(function* (req, res) {
    try {
        let id = req.body.id;
        if (!id) return res.send(msg.genFailedMsg('删除失败,ID不允许为空'))
        let fieldinfo = yield SysField.load(id)
        console.log(fieldinfo)
        yield fieldinfo.remove()
        res.send(msg.genSuccessMsg('删除成功'))
    } catch (error) {
        console.log(error);
        res.send(msg.genFailedMsg('删除失败'))
    }
})
//获取所有字段信息的名称
exports.GetAllFieldName = async(function* (req, res) {
    try {
        list = yield SysField.find().select('SysTabName').exec()
        res.send(msg.genSuccessMsg('读取字段管理列表成功', list))
    } catch (error) {
        res.send(msg.genFailedMsg('获取字段管理列表失败', error))
    }
})