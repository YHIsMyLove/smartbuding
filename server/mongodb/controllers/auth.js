const msg = require('../../utils/message')
//const _cos = require('../../utils/getCOSauth')
exports.cosauth = function (req, res) {
    var method = _cos.getParam(req.url, 'method');
    var pathname = decodeURIComponent(getParam(req.url, 'pathname'));
    var auth = _cos.getAuthorization(method, pathname);
    console.log(method, pathname);
    res.writeHead(200, {
        'Content-Type': 'text/plain',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'OPTIONS,GET,POST',
        'Access-Control-Allow-Headers': 'accept,content-type',
        'Access-Control-Max-Age': 60
    })
        .write(auth || '')
        .end()
}