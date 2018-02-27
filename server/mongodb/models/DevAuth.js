const mongoose = require('mongoose')
const Schema = mongoose.Schema

// areaid|projid|projname
// --|--|--
// 01|01|XX项目
// 02|02|xXX项目
// 03|03|xxXX项目
// project
const devAuthSchema = new Schema({
    authid:String,
    authDesc:String,
    roleid:String,
    devid:String,
    operation:String
})

devAuthSchema.pre('save', function (next) {
    next()
})
devAuthSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
devAuthSchema.statics = {
    fetch: function () { },
    findById: function () { },
    load: function (id) {
        return this.findOne({ _id: id }).exec();
    },
    list: function (options) {
        const criteria = options.criteria || {};
        const page = options.page || 0;
        const limit = options.limit || 30;
        return this.find({})
            .sort({ createdAt: -1 })
            .limit(limit)
            .skip(limit * page)
            .exec();
    }
}
mongoose.model('DevAuth', devAuthSchema);
