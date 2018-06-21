const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//审批表
const approveSchema = new Schema({
    ProjID: String,
    proposer: String,
    approver: String,
    approveState: { type: Number, default: 0 },
    startTime: { type: Date, default: Date.now },
    approveContent: Schema.Types.Mixed,
})

approveSchema.pre('save', function (next) {
    next()
})
approveSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
approveSchema.statics = {
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
mongoose.model('Approve', approveSchema);
