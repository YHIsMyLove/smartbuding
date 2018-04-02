const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//会议表
const MeetingSchema = new Schema({
    MeetingName: String,
    MeetingCreatedAt: { type: Date, default: Date.now },
    Compere: String,//主持人
    ProjID: String
})

MeetingSchema.pre('save', function (next) {
    next()
})
MeetingSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
MeetingSchema.statics = {
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
mongoose.model('Meeting', MeetingSchema);
