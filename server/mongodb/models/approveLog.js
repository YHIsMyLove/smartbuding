const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//审批记录表
const approveLogSchema = new Schema({
    _approveID: Schema.Types.ObjectId,
    proposer: Schema.Types.Mixed,
    approver: Schema.Types.Mixed,
    overseer: Schema.Types.Mixed
})

approveLogSchema.pre('save', function (next) {
    next()
})
approveLogSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
approveLogSchema.statics = {
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
mongoose.model('ApproveLog', approveLogSchema);
