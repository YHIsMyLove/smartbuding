const config = require('../config/config.js')
const ServerUrl = `http://openapi.xg.qq.com/v2/class/method?params`
const axios = require('axios')
/**
 * 推送android平台
 */
let pushAndroid = async () => {
    let _sign = `${ServerUrl}`
    let params = {
        access_id: config.XG_AccessID,
        cal_type: 0,
        timestamp: Date.now(),
        sign: _sign
    }
    return new Promise(async (res, rej) => {
        let result = await axios.post(ServerUrl)
        if (result) {
        }
    })
}


console.log()