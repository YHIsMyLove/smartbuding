const mongoose = require('mongoose')
const Schema = mongoose.Schema

// areaid|projid|projname
// --|--|--
// 01|01|XX项目
// 02|02|xXX项目
// 03|03|xxXX项目
// project
const projSchema = new Schema({
    areaid:String,
    projid:String,
    projname:String
})

projSchema.pre('save', function (next) {
    next()
})
projSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
projSchema.statics = {
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
mongoose.model('Proj', projSchema);
