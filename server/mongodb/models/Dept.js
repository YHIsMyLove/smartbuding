const mongoose = require('mongoose')
const Schema = mongoose.Schema

// deptid|deptname|areaid
// --|--|--
// 01|01部|01
// 02|02部|01
// 03|01部|02
//bumen
const deptSchema = new Schema({
    deptid:String,
    deptname:String,
    areaid:String,
    parentid:String
})

deptSchema.pre('save', function (next) {
    next()
})
deptSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
deptSchema.statics = {
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
mongoose.model('Dept', deptSchema);
