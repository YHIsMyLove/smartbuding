const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//设备表
const deviceSchema = new Schema({
    DevID: String,
    DevName: String,
    DevStatus: Number,
    DevType: String,
    DevDesc: String,
    DevClass: String,
    DevIp: String,
    DevPort: Number,
    ProjID: String
})

deviceSchema.pre('save', function (next) {
    next()
})
deviceSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
deviceSchema.statics = {
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
mongoose.model('Device', deviceSchema);
