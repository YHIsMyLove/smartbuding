var moment = require('moment')
/************************************************************************** */
//0 代表循环中 1 结束 
var run_loop = 0;
var count = 1
var time_now = moment()
// 剩余时间
var time_diff = 0
var time_end = time_now.subtract(-0.2, "minute")
var list_user_seckill = []
var user_seckill = {}

function socketVerify(info) {
    console.log(info.origin);
    console.log(info.req.t);
    console.log(info.secure);
    return true; //否则拒绝  
}
var WebSocketServer = require('ws').Server,
    wss = new WebSocketServer({
        port: 8181,
        verifyClient: socketVerify
    });

console.log('服务正在启动...')
wss.on('connection', (ws) => {
    console.log('client connected');
    //ws.send('你是第' + wss.clients.length + '位');
    ws.on('message', (message) => {
        if (run_loop == 1) {
            ws.send('服务器休息中,等待下次开启')
            return;
        }
        var client = JSON.parse(message)
        console.log(client.name)
        user_seckill = {
            name: client.name,
            seckilled: 0,//0 未参加 1 参加未获得资格 2 参加获得资格
            seckill_num: count,
        }
        if (client.msg == 'test') {
            var all_count = list_user_seckill.filter(i => i.seckill_num == count).length
            var findone = list_user_seckill.filter(i => i.name == user_seckill.name && i.seckilled == 2 /*&& i.seckill_num == count*/)
            if (findone.length == 0 && all_count <= 1) {
                user_seckill.seckilled = 2
                ws.send(`${user_seckill.name}:秒杀成功!`)
                list_user_seckill.push(user_seckill)
                //先到先得?还是什么权重
            }
            else if (findone.length == 0 && all_count > 1) {
                user_seckill.seckilled = 1
                ws.send(`${user_seckill.name}:秒杀失败...请下次再来!`)
                list_user_seckill.push(user_seckill)
            }
            else {
                ws.send(`${user_seckill.name}:秒杀失败...已经参加过活动啦!`)
            }
        }
    })
    ws.on('error', (error) => {
        console.log(error)
    })
    ws.on('close', (close) => {
        console.log(close)
    })
    var clientStockUpdater = setInterval(() => {
        if (run_loop == 0 && ws.readyState != 3) {
            var msg = `[time]当前第${count}轮,剩余时间${time_diff},参与人数${list_user_seckill.filter(i => i.seckill_num == count).length}个`
            ws.send(msg)
        }
    }, 1000);
});
/*************************************** */

console.log(`第${count}次秒杀活动开启!`)
var activeAction = setInterval(() => {
    activeAction_interval(time_end)
}, 1000)
var activeAction_interval = (endtime) => {
    time_now = moment()
    time_diff = Math.round(endtime.diff(time_now, 'ms') / 1000)
    console.log(`当前时间${time_now.format('hh:mm:ss')} 结束时间${endtime.format('hh:mm:ss')} 时间差${time_diff}`)
    if (time_diff <= 0) {
        run_loop = 1
        clearInterval(activeAction)
        console.log(`第${count}轮秒杀活动结束!`)
        count++
        var next_activeAction = setTimeout(() => {
            run_loop = 0;
            endtime = moment().subtract(-0.2, 'minute')
            console.log(`第${count}次秒杀活动开启!`)
            activeAction = setInterval(() => {
                activeAction_interval(endtime)
            }, 1000)
        }, 5000)
    }
}