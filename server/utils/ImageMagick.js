var gm = require('gm')
    , fs = require('fs')
    , imageMagick = gm.subClass({ imageMagick: true });

//生成缩略图 -----mustbe install imagemagick
exports.resizeImg = function (path = '../static/a.png') {
    let r_path = '../static/a_x150.png'
    return new Promise((res, rej) => {
        imageMagick(path)
            .resize(150, 150, '!')
            .write(r_path, function (err) {
                if (!err) res()
                else rej(err)
            });
    })
}

//resizeImg().then(res => { console.log('转换成功') }).catch(err => console.log('转换失败->' + err))

