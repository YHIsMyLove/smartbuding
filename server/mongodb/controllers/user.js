const mongoose = require('mongoose');
const User = mongoose.model('User');
const { wrap: async } = require('co');
const msg = require('../../utils/message')

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
exports.list = async(function* (req, res) {
    try {
        var query = {
            page: parseInt(req.query.page) - 1,
            limit: parseInt(req.query.limit)
        }
        var list = yield User.list(query);
        console.log(list)
        var count = yield User.count();
        res.send(msg.genSuccessMsg('读取用户列表成功', list, { count: count }))
    } catch (error) {
        console.log(error);
        res.send(msg.genFailedMsg('获取用户列表失败'))
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
exports.checklogin = async(function* (req, res) {
    if (Object.keys(req.body).length === 0) {
        return res.send(msg.genFailedMsg('body不能为空'))
    }
    console.log(req.body.account)
    console.log(req.body.checkPass)
    if ((req.body.account && req.body.account === 'admin') &&
        (req.body.checkPass && req.body.checkPass === 'admin')) {
        return res.send(msg.genSuccessMsg('登录成功'))
    }
    return res.send(msg.genFailedMsg('登录失败'))
})
/******************************************************************** */
const axios = require('axios')
const jwt = require('jsonwebtoken')
const config = require('../../config/config')
let generateToken = function (user) {
    return jwt.sign(user, config.jwtSecret, {
        expiresIn: 7200
    })
}
exports.login = async(function* (req, res) {
    const queryString = `appid=${config.appId}&secret=${config.appSecret}&js_code=${req.body.code}&grant_type=authorization_code`;
    const wxAPI = `https://api.weixin.qq.com/sns/jscode2session?${queryString}`;
    var user_info = req.body.info
    console.log(user_info)
    axios.get(wxAPI)
        .then(response => {
            console.log(response.data);
            User.findOne({ openId: response.data.openid }, (err, user) => {
                if (user) {
                    console.log('登录' + JSON.stringify(response.data))
                    return res.send(msg.genSuccessMsg('已登录', { openid: response.data.openid }))
                } else {
                    const user = new User();
                    console.log('登录' + JSON.stringify(response.data))
                    user.openId = user_info.openid;
                    user.name = user_info.nickName;
                    user.addr = `${user_info.country}-${user_info.province}+${user_info.city}`
                    user.headimgurl = user_info.avatarUrl;
                    user.sex = user_info.gender
                    user.save();
                    console.log(user_info)
                    return res.send(msg.genSuccessMsg('已登录', { openid: response.data.openid }))
                }
            })
        })
        .catch(error => {
            return res.send(msg.genFailedMsg(error))
        })
})
exports.checkToken = async(function* (req, res) {
    let token = req.headers.authorization;
    console.log(token);
    if (token) {
        jwt.verify(token, config.jwtSecret, (err, decoded) => {
            if (err) {
                console.log('err');
                if (err.name === 'TokenExpiredError') {
                    return res.send(msg.genFailedMsg('认证码失效，请重新登录!'))
                } else {
                    return res.send(msg.genFailedMsg('认证失败！'))
                }
            } else {
                if (decoded.openid) {
                    req.openid = decoded.openid;
                    // console.log('req.openid = decoded.openid;');
                    // return res.status(200).json({ message: '已登录' });
                    return res.send(msg.genSuccessMsg('已登录'))
                } else {
                    return res.send(msg.genFailedMsg('认证失败！'))
                    // console.log('认证失败！');
                    // res.status(401).json({ error: '认证失败！' });
                }
            }
        });
    } else {
        return res.send(msg.genFailedMsg('请提供认证码！'))
    }
})

