
const axios = require('axios')
const qs = require('qs')
const config = require('../config/config')


//获取萤石的token
exports.getaccessToken = async (callback) => {
    var accessToken = ''
    let result = await axios.post('https://open.ys7.com/api/lapp/token/get', qs.stringify({
        appKey: config.Ys_AppID,
        appSecret: config.Ys_AppSecret
    }))
    if (result.data.code == '200') {
        accessToken = result.data.data.accessToken
        callback(accessToken, result.data.msg)
    } else {
        callback('', result.data.msg)
    }
}

//getaccessToken((t, m) => console.log(t))