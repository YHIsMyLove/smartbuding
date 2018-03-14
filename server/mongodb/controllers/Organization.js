// const mongoose = require('mongoose');
// const { wrap: async } = require('co');
// const msg = require('../../utils/message')
// const SystemConfig = require('../../config/config')
// const Mock = require('mockjs')

// const user = mongoose.model('User')
// const dept = mongoose.model('Dept')
// const post = mongoose.model('Post')

// //get dept list [root layer]
// exports.deptlist = async(function*(req,res){
//     let query = {}
//     let data = {}
//     res.send(msg.genSuccessMsg('get dept list ok', data))
// })

// /*dept******************************************************** */
// exports.adddept = async(function*(req,res){
//     res.send(msg.genSuccessMsg('get dept list ok'))
// })
// exports.deldept = async(function*(req,res){
//     res.send(msg.genSuccessMsg('get dept list ok'))    
// })
// exports.editdept = async(function*(req,res){
//     res.send(msg.genSuccessMsg('get dept list ok'))    
// })
// /*post******************************************************** */
// exports.addpost = async(function*(req,res){
//     res.send(msg.genSuccessMsg('get post list ok'))
// })
// exports.delpost = async(function*(req,res){
//     res.send(msg.genSuccessMsg('get post list ok'))    
// })
// exports.editpost = async(function*(req,res){
//     res.send(msg.genSuccessMsg('get post list ok'))    
// })
// /*post******************************************************** */
// //just a test
// exports.testpost = async function(req,res){
//     await res.send(msg.genFailedMsg('test'))
// }