var nodemailer = require('nodemailer')
var smtpTransport = require('nodemailer-smtp-transport');
var config = require('../config/config')

smtpTransport = nodemailer.createTransport(smtpTransport({
    service: config.EmailServer,
    auth: {
        user: config.EmailUser,
        pass: config.EmailPwd
    }
}));

/**
 * @param {String} recipient 收件人
 * @param {String} subject 发送的主题
 * @param {String} html 发送的html内容
 * @param {[{filename:'',path:''}]} attachments 发送的附件
 */
exports.SendMail = (recipient, subject = '', html = '', attachments = []) => {
    smtpTransport.sendMail({
        from: config.EmailUser,
        to: recipient,
        subject: subject,
        html: html,
        attachments: attachments
    }, function (error, response) {
        if (error) {
            console.log(error);
        }
        console.log('发送成功')
    });
}


