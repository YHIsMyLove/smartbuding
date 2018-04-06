const mongoose = require('mongoose');
const User = mongoose.model('User');
const { wrap: async } = require('co');
const msg = require('../../utils/message')
const SystemConfig = require('../../config/config')
const Mock = require('mockjs')


exports.getUsers = async(function* (req, res) {

    
})