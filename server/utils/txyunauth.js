// 引入模块
var COS = require('cos-nodejs-sdk-v5');
// 创建实例
var cos = new COS({
    AppId: '1250000000',
    SecretId: 'AKIDslYFqLM2HSix574qncGjwypUxFOM2lDU',
    SecretKey: 'FJLH7qSyNh4Qgp061mPtCKVWLyQktsgO',
});

//上传文件
exports.uploadfile = function (params) {
    // 分片上传
    cos.sliceUploadFile({
        Bucket: 'test',
        Region: 'ap-guangzhou',
        Key: params.Key,
        FilePath: `./${params.Key}`
    }, function (err, data) {
        console.log(err, data);
    });
}