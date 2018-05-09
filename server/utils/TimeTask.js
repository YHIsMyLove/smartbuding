var later = require('later')
var process = require('child_process');
var config = require('../config/config')
var moment = require('moment')
var fs = require('fs')
var os = require('os')
var zipper = require("zip-local");
var mail2 = require('./EmailSender')

var sched = later.parse.text('every 10 sec')

var task = () => {
    later.setInterval(() => {
        var time = moment().format('YYYYMMDDhhmmss')
        var backuppath = `${__dirname}/../${config.MongoDB_BackUpPath}/${time}`
        fs.exists(backuppath, exists => {
            if (!exists) {
                fs.mkdir(backuppath, (err) => {
                    if (err) return
                    process.exec(`mongodump -h 127.0.0.1:${config.MongoDB_Port} -d ${config.MongoDB_Path} -o ${backuppath}`,
                        (err, stdout, stderr) => {
                            if (err) {
                                console.log(err)
                            } else {
                                var zippath = `${backuppath}.zip`
                                zipper.sync
                                    .zip(backuppath)
                                    .compress()
                                    .save(zippath);
                                var context = `${moment().format('YYYY年MM月DD日 hh:mm:ss定时备份数据库完成!')}`
                                mail2.SendMail(
                                    '331446704@qq.com',
                                    '数据库备份',
                                    context,
                                    [
                                        {
                                            filename: time,
                                            path: zippath
                                        }
                                    ])
                                if (stderr) {
                                    console.log(stderr)
                                } else {
                                    console.log(stdout)
                                }
                            }
                        })
                })
            } else {
                console.log('已存在此文件夹')
            }
        })
    }, sched)
}
//task()
exports.MongoDBBackUpTask = task


