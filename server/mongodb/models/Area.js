const mongoose = require('mongoose')
const Schema = mongoose.Schema

// areaid|areaname  
// --|--  
// 01|北京
// 02|上海
// 03|广州
// 04|深圳
//区域表
const areaSchema = new Schema({
    areaid:String,
    areaname:String
})

areaSchema.pre('save', function (next) {
    next()
})
areaSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
areaSchema.statics = {
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
mongoose.model('Area', areaSchema);
