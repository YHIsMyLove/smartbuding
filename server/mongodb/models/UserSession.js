const mongoose = require('mongoose')
const Schema = mongoose.Schema

//区域表
const userSessionSchema = new Schema({
    UserID: String,
    UserLoginDate: { type: Date, default: Date.now }
})

userSessionSchema.pre('save', function (next) {
    next()
})
userSessionSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
userSessionSchema.statics = {
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
mongoose.model('UserSession', userSessionSchema);
