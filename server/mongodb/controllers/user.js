const mongoose = require('mongoose');
const User = mongoose.model('User');
const { wrap: async } = require('co');
const msg = require('../../utils/message')
const SystemConfig = require('../../config/config')

exports.load = async(function* (req, res, next, id) {
    if (!id) {
        return res.send(msg.genFailedMsg('id不能为空'))
    }
    try {
        req.user = yield User.load(id)
        //new Promise().then().then().catch().then()
        if (!req.user) return next(new Error('Use not found'));
    } catch (error) {
        return next(error);
    }
    next()
})
exports.create = async(function* (req, res) {
    console.log(req.body)
    if (Object.keys(req.body).length === 0) {
        return res.send(msg.genFailedMsg('body不能为空'))
    }
    let user = new User(req.body);
    try {
        yield user.updateAndSave();
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        console.log(error);
        res.send(msg.genFailedMsg('保存失败'))
    }
})
exports.update = async(function* (req, res) {
    console.log('修改')
    try {
        let user = req.user;
        console.log(JSON.stringify(req.user))
        user = Object.assign(user, req.body);
        yield user.updateAndSave();
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        console.log(error);
        res.send(msg.genFailedMsg('保存失败'))
    }
})
exports.delete = async(function* (req, res) {
    try {
        let user = req.user;
        yield user.remove()
        res.send(msg.genSuccessMsg('删除成功', user))
    } catch (error) {
        console.log(error);
        res.send(msg.genFailedMsg('删除失败'))
    }
})

//检查登录
exports.checklogin = async(function* (req, res) {
    if (Object.keys(req.body).length === 0) {
        return res.send(msg.genFailedMsg('body不能为空'))
    }
    if ((req.body.account && req.body.account === SystemConfig.Admin_User) &&
        (req.body.checkPass && req.body.checkPass === SystemConfig.Admin_Pwd)) {
        return res.send(msg.genSuccessMsg('登录成功'))
    }
    return res.send(msg.genFailedMsg('登录失败'))
})

const Mock = require('mockjs')
//分页获取人员数据
exports.list = async(function* (req, res) {
    // if (SystemConfig.MockData == 1) {
    //     console.log('mock数据.........................')
    //     var Random = Mock.Random;
    //     var data = Mock.mock({
    //         // 属性 list 的值是一个数组，其中含有 1 到 10 个元素
    //         'data|20': [{
    //             'User': {
    //                 // 属性 id 是一个自增数，起始值为 1，每次增 1
    //                 '_id|+1': 1,
    //                 'UserID|+1': 1,
    //                 'UserName': Random.string(2, 4),
    //                 'UserSex': Random.integer(0, 1),
    //                 'UserAge': Random.integer(20, 40),
    //                 'UserBirth': Random.date('yyyy-mm-dd'),
    //                 'UserAddr': Random.county(),
    //                 'UserPhoneNum': Random.integer(0, 1),
    //             }
    //         }]
    //     })
    //     return res.send(msg.genSuccessMsg('读取用户列表成功', data, { count: 20 }))
    // }
    try {
        var query = {
            page: parseInt(req.query.page) - 1,
            limit: parseInt(req.query.limit)
        }
        var list = yield User.list(query);
        var count = yield User.count();
        res.send(msg.genSuccessMsg('读取用户列表成功', list, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('获取用户列表失败'))
    }
})