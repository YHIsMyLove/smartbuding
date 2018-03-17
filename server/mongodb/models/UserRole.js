const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户角色表
const userRoleSchema = new Schema({
    UserID: String,
    RoleID: String,
    ProjID: String
})

userRoleSchema.pre('save', function (next) {
    next()
})
userRoleSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
userRoleSchema.statics = {
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
mongoose.model('UserRole', userRoleSchema);
