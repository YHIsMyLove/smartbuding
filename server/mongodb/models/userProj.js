const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const userProjSchema = new Schema({
    UserID: String,
    ProjID: String,
    DeptID: String
})

userProjSchema.pre('save', function (next) {
    next()
})
userProjSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
userProjSchema.statics = {
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
mongoose.model('UserProj', userProjSchema);
