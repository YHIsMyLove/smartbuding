const mongoose = require('mongoose')
const Schema = mongoose.Schema;

const caseShowSchema = new Schema({
    CSID: String,//
    CSTitle: String,//标题
    CSTime: { type: Date, default: Date.now },
    CSImage: String,//缩略图
    CSVRPath: String,//VR路径
    UserID: String,//关联用户id
    OrderID: String,//关联订单号id
    MessageID: String,//关联留言id
})

caseShowSchema.pre('save', function (next) {
    next()
})

caseShowSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}

caseShowSchema.statics = {
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
mongoose.model('CaseShow', caseShowSchema);