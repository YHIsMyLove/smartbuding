const mongoose = require('mongoose')
const Schema = mongoose.Schema;

const SysFieldSchema = new Schema({
    SysTabName: String,
    SysFieldInfo: mongoose.Schema.Types.Mixed
})

SysFieldSchema.pre('save', function (next) {
    next()
})

SysFieldSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}

SysFieldSchema.statics = {
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
        return this.find({})//criteria
            .sort({ createdAt: -1 })
            .limit(limit)
            .skip(limit * page)
            .exec();
    }
}
//注册mongoose模型，其他地方可以直接 mongoose.model('User)调用
mongoose.model('SysField', SysFieldSchema);
