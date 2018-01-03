const mongoose = require('mongoose')
const Schema = mongoose.Schema;

const SysTableSchema = new Schema({
    SysFieldID: String,
    item0: String,
    item1: String,
    item2: String,
    item3: String,
    item4: String,
    item5: String,
    item6: String,
    item7: String,
    item8: String,
    item9: String,
})

SysTableSchema.pre('save', function (next) {
    next()
})

SysTableSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}

SysTableSchema.statics = {
    fetch: function () {

    },
    findById: function () {

    },
    load: function (id) {
        return this.findOne({ _id: id })
            .exec();
    },
    list: function (options) {
        const criteria = options.criteria || {};
        const page = options.page || 0;
        const limit = options.limit || 30;
        const SysFieldID = options.SysFieldID
        return this.find({ SysFieldID: SysFieldID })//criteria
            .sort({ createdAt: -1 })
            .limit(limit)
            .skip(limit * page)
            .exec();
    }
}
//注册mongoose模型，其他地方可以直接 mongoose.model('User)调用
mongoose.model('SysTable', SysTableSchema);
