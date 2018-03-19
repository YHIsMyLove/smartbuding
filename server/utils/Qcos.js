const config = require('../config/config')
const COS = require('cos-nodejs-sdk-v5');
const cos = new COS({
    SecretId: config.Qcos_SecretId,
    SecretKey: config.Qcos_SecretKey,
});
/******
 * 1. cos.getService 接口实现获取该用户下所有 Bucket 列表。
 * 该 API 接口需要使用 Authorization 签名认证，
 * 且只能获取签名中 AccessID 所属账户的 Bucket 列表。
 * 
 * region['ap-beijing-1','ap-beijing','ap-shanghai','ap-guangzhou','ap-chengdu']
 * 2. headBucket
 * 3. putBucket 创建Bucket params{bucket,region}
 * 4. deleteBucket 删除Bucket params{bucket,region}
 * 5. getObject 获取实体 params{bucket,region,key} 
 * 6. putObject 上传实体 params{bucket,region,key,body}
 * 7. sliceUploadFile 分片上传 params{bucket,region,key,filepath}
 * 8. deleteObject 删除实体 params{bucket,region,key} 
 */
exports.Upload = async (key) => {
    let result = await cos.sliceUploadFile({
        Bucket: config.Qcos_Bucket,
        Region: config.Qcos_Region,
        key: key,
        FilePath: `./${key}`
    })
    return result
}

//putBucket

exports.GetFiles = async (query) => {
    let params = {
        Bucket: config.Qcos_Bucket,
        Region: config.Qcos_Region,
        MaxKeys: query.currentPageSize,//单次返回最大的条目数量,
        //Prefix: '',//模糊搜索
    };
    let result = await cos.getBucket(params)
    return result
}