const mongoose = require('mongoose');
const CaseShow = mongoose.model('CaseShow');
const { wrap: async } = require('co');
const msg = require('../../utils/message')
var multiparty = require('multiparty');

/**
 * 分页获取案例展示列表
 */
exports.GetCase = async(function* (req, res) {
    try {
        var query = {
            page: parseInt(req.query.page) - 1,
            limit: parseInt(req.query.limit)
        }
        var list = yield CaseShow.list(query);
        var count = yield CaseShow.count();
        res.send(msg.genSuccessMsg('读取案例展示列表成功', list, { count: count }))
    } catch (error) {
        console.log(error);
        res.send(msg.genFailedMsg('获取案例展示列表失败'))
    }
})
exports.GetCaseByUserID = function (req, res) {
    res.send('GetCaseByUserID' + req.query.UserID)
}
exports.SaveCase = function (req, res) {
    res.send('SaveCase')
}
exports.DelCaseByID = function (req, res) {
    res.send('DelCaseByID' + req.query.UserID)
}
exports.test = function (req, res) {
    res.send('SaveCase')
}
//上传图片
exports.upload = async(function* (req, res) {
    console.log('有没有调用我啊')
    res.send(msg.genSuccessMsg('成功'));
    //生成multiparty对象，并配置上传目标路径
    // var form = new multiparty.Form({ uploadDir: './static' });
    // //上传完成后处理
    // form.parse(req, function (err, fields, files) {
    //     console.log(err)
    //     var inputFile = files.file[0];
    //     var uploadedPath = inputFile.path;
    //     var dstPath = './static/' + inputFile.originalFilename;
    //     fs.rename(uploadedPath, dstPath, function (err) {
    //         if (err) {
    //             console.log('rename error: ' + err);
    //         } else {
    //             console.log('rename ok');
    //         }
    //     });
    //     files.file.path = dstPath;
    //     var data = files;
    //     qcos.upload(files.file.path, (err, result) => {
    //         if (err) {
    //             return res.send(msg.genFailedMsg(err))
    //         }
    //         res.send(msg.genSuccessMsg('成功', result));
    //         //删除本地?
    //     })
    // });
})